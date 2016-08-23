using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snippy.Data.UnitOfWork;
using AutoMapper;
using Snippy.App.Models.ViewModels;
using Snippy.App.Models.BindingModels;
using Snippy.Models;
using Microsoft.AspNet.Identity;

namespace Snippy.App.Controllers
{
    public class SnippetsController : BaseController
    {
        public SnippetsController(ISnippyData data) : base(data)
        {
        }

        public ActionResult Index(int page = 1, int count = 5)
        {
            int snippetsCount = this.Data.Snippets.All().Count();

            var snippets = this.Data.Snippets.All()
                .OrderByDescending(s => s.CreationDate)
                .Skip((page - 1) * count)
                .Take(count)
                .ToList();

            this.ViewBag.TotalPages = (snippetsCount + count - 1) / count;
            this.ViewBag.CurrentPage = page;

            var snippetsModel = Mapper.Map <IEnumerable<ConciseSnippetVM>>(snippets);

            return View(snippetsModel);
        }

        public ActionResult GetSnippet(string title)
        {
            var snippet = this.Data.Snippets.All().FirstOrDefault(s => s.Title == title);
            if (snippet == null)
            {
                TempData["message"] = "No such snippet available!";
                return View("../Shared/Error");
            }

            var model = Mapper.Map<FullSnippetVM>(snippet);

            return this.View("SnippetDetails", model);
        }

        [Authorize]
        public ActionResult Create()
        {
            var languages = this.Data.Languages.All().Select(l => l.Name).ToList();
            TempData["languages"] = new SelectList(languages);
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateSnippetBM model)
        {
            if (!ModelState.IsValid)
            {
                var languages = this.Data.Languages.All().Select(l => l.Name).ToList();
                TempData["languages"] = new SelectList(languages);
                TempData["error"] = "Invalid snippet.";
                return this.View();
            }

            if (this.Data.Snippets.All().Any(s => s.Title == model.Title))
            {
                var languages = this.Data.Languages.All().Select(l => l.Name).ToList();
                TempData["languages"] = new SelectList(languages);
                TempData["error"] = "A snippet with this title already exists!";
                return this.View();
            }

            var labels = model.Labels.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            var newSnippet = new Snippet()
            {
                Title = model.Title,
                Description = model.Description,
                Code = model.Code,
                CreationDate = DateTime.Now,
                AuthorId = this.User.Identity.GetUserId()
            };

            var language = this.Data.Languages.All().First(l => l.Name == model.Language);
            newSnippet.LanguageId = language.LanguageId;
            newSnippet.Language = language;

            var dbLabels = this.Data.Labels.All().Where(l => labels.Contains(l.Text)).ToList();
            var nonDbLabels = labels.Where(l => !dbLabels.Any(la => la.Text == l)).ToList();

            foreach (var lab in nonDbLabels)
            {
                var newLabel = new Label()
                {
                    Text = lab
                };

                this.Data.Labels.Add(newLabel);
                this.Data.Labels.SaveChanges();
                dbLabels.Add(newLabel);
            }
            
            newSnippet.Labels = dbLabels;

            this.Data.Snippets.Add(newSnippet);
            this.Data.Snippets.SaveChanges();

            return RedirectToAction("GetSnippet", routeValues: new { title = model.Title });
        }

        [Authorize]
        public ActionResult Edit(int snippetId)
        {
            var languages = this.Data.Languages.All().Select(l => l.Name).ToList();
            TempData["languages"] = new SelectList(languages);
            var snippet = this.Data.Snippets.Find(snippetId);
            var snippetModel = Mapper.Map<CreateSnippetBM>(snippet);
            return View(snippetModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(CreateSnippetBM model)
        {
            if (!ModelState.IsValid)
            {
                var languages = this.Data.Languages.All().Select(l => l.Name).ToList();
                TempData["languages"] = new SelectList(languages);
                TempData["error"] = "Invalid snippet.";
                return this.View();
            }

            var currSnippet = this.Data.Snippets.Find(model.SnippetId);

            var labels = model.Labels.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            currSnippet.Title = model.Title;
            currSnippet.Description = model.Description;
            currSnippet.Code = model.Code;
            currSnippet.CreationDate = DateTime.Now;
            currSnippet.AuthorId = this.User.Identity.GetUserId();

            var language = this.Data.Languages.All().First(l => l.Name == model.Language);
            currSnippet.LanguageId = language.LanguageId;
            currSnippet.Language = language;

            var dbLabels = this.Data.Labels.All().Where(l => labels.Contains(l.Text)).ToList();
            var nonDbLabels = labels.Where(l => !dbLabels.Any(la => la.Text == l)).ToList();

            foreach (var lab in nonDbLabels)
            {
                var newLabel = new Label()
                {
                    Text = lab
                };

                this.Data.Labels.Add(newLabel);
                this.Data.Labels.SaveChanges();
                dbLabels.Add(newLabel);
            }
            
            this.Data.Snippets.SaveChanges();

            return RedirectToAction("GetSnippet", routeValues: new { title = model.Title });
        }
    }
}
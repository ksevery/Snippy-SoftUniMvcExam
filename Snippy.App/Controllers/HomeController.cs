using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snippy.Data.UnitOfWork;
using Snippy.App.Models.ViewModels;
using AutoMapper;

namespace Snippy.App.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ISnippyData data) : base(data)
        {
        }

        public ActionResult Index()
        {
            // 5 latest snippets
            var snippets = this.Data.Snippets.All()
                .OrderByDescending(s => s.CreationDate)
                .Take(5);

            // 5 latest comments
            var comments = this.Data.Comments.All()
               .OrderByDescending(c => c.CreationTime)
               .Take(5);

            // 5 best labels
            var labels = this.Data.Labels.All()
               .OrderByDescending(l => l.Snippets.Count)
               .Take(5);

            var homepageModel = new HomePageVM()
            {
                Snippets = Mapper.Map<IEnumerable<ConciseSnippetVM>>(snippets),
                Comments = Mapper.Map<IEnumerable<ConciseCommentVM>>(comments),
                Labels = Mapper.Map<IEnumerable<ConciseLabelVM>>(labels)
            };

            return View(homepageModel);
        }
    }
}
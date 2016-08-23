using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snippy.Data.UnitOfWork;
using AutoMapper;
using Snippy.App.Models.ViewModels;

namespace Snippy.App.Controllers
{
    public class LanguagesController : BaseController
    {
        public LanguagesController(ISnippyData data) : base(data)
        {
        }

        public ActionResult Details(string language)
        {
            var lang = this.Data.Languages.All().FirstOrDefault(l => l.Name == language);
            if (lang == null)
            {
                TempData["message"] = "No such language!";
                return View("Error");
            }

            var langModel = Mapper.Map<LanguageDetailsVM>(lang);
            return View(langModel);
        }
    }
}
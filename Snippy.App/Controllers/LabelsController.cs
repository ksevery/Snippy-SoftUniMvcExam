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
    public class LabelsController : BaseController
    {
        public LabelsController(ISnippyData data) : base(data)
        {
        }

        // GET: Labels
        public ActionResult Details(string labelName)
        {
            var label = this.Data.Labels.All().FirstOrDefault(l => l.Text == labelName);
            if (label == null)
            {
                TempData["message"] = "No such label!";
                return View("Error");
            }

            var labelModel = Mapper.Map<LabelDetailsVM>(label);
            return View(labelModel);
        }
    }
}
using Snippy.App.Models.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snippy.App.Controllers
{
    public class CommentsController : Controller
    {
        public ActionResult Create()
        {
            return PartialView("_CommentCreatePartial");
        }

        [HttpPost]
        public ActionResult Create(CommentBM model)
        {
            return Content("Goshoooooooo!!!");
        }
    }
}
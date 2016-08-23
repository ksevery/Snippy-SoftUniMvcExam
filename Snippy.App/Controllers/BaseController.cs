using Snippy.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snippy.App.Controllers
{
    public class BaseController : Controller
    {
        private ISnippyData data;

        public BaseController(ISnippyData data)
        {
            this.Data = data;
        }

        protected ISnippyData Data
        {
            get
            {
                return this.data;
            }
            private set
            {
                this.data = value;
            }
        }
    }
}
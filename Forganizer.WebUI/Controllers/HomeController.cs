using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Forganizer.DomainModel.Abstract;

namespace Forganizer.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }

        public ViewResult Dashboard()
        {
            return View();
        }
    }
}

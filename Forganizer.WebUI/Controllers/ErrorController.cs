using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Net;
using Naspinski.MVC.Performance;

namespace Forganizer.WebUI.Controllers
{
    [EnableCompression]
    public class ErrorController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult NotFound(string url, string aspxerrorpath) 
        { 
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            ViewData["url"] = string.IsNullOrEmpty(url) ? aspxerrorpath : url;
            TempData["error"] = Response.Status;
            return View(); 
        }
    }
}

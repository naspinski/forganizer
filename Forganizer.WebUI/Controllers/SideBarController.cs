using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Forganizer.DomainModel.Entities;
using Forganizer.WebUI.Models;

namespace Forganizer.WebUI.Controllers
{
    [HandleError]
    public class SideBarController : Controller
    {
        public ActionResult Index(FileAndTagCollection fileAndTagCollection, SearchType tagAndOr)
        {
            fileAndTagCollection.RouteData = this.RouteData;
            ViewData["tagAndOr"] = tagAndOr;//.ToString();
            return View(fileAndTagCollection);
        }
    }
}

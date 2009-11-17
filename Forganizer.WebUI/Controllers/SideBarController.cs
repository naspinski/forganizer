using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Forganizer.DomainModel.Entities;
using Forganizer.WebUI.Models;

namespace Forganizer.WebUI.Controllers
{
    public class SideBarController : Controller
    {
        public ActionResult Index(FileAndTagCollection fileAndTagCollection, SearchType tagAndOr)
        {
            fileAndTagCollection.RouteData = this.RouteData;
            //SearchType TagAndOr = tagAndOr == SearchType.Or.ToString() ? SearchType.Or : SearchType.And;
            ViewData["tagAndOr"] = tagAndOr;//.ToString();
            return View(fileAndTagCollection);
        }
    }
}

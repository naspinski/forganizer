using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Forganizer.DomainModel.Entities;
using Forganizer.WebUI.Models;

namespace Forganizer.WebUI.Controllers
{
    public class SideBarController : Controller
    {
        public ActionResult Index(FileAndTagCollection fileAndTagCollection, string tags, string extensions, int page, string tagAndOr)
        {
            ViewData["tags"] = tags;
            ViewData["extensions"] = extensions;
            SearchType TagAndOr = tagAndOr == SearchType.Or.ToString() ? SearchType.Or : SearchType.And;
            ViewData["tagAndOr"] = TagAndOr.ToString();
            ViewData["page"] = page;
            return View(fileAndTagCollection);
        }
    }
}

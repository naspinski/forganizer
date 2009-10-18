using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
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
            return View(fileAndTagCollection);
        }

        public ActionResult Container(IEnumerable<Tag> tagObjects, string tags, string extensions, string tagAndOr, bool active, TagType tagType)
        {
            ViewData["tags"] = tags;
            ViewData["extensions"] = extensions;
            ViewData["tagAndOr"] = tagAndOr;
            ViewData["tagType"] = tagType;

            return View(tagObjects.Where(x => x.Active == active));
        }
    }
}

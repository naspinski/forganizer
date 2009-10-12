using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Forganizer.DomainModel.Abstract;
using Forganizer.DomainModel.Entities;
using Forganizer.DomainModel.Extensions;
using Forganizer.DomainModel;
using Forganizer.WebUI.App_Code;
using System.IO;

namespace Forganizer.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private int PageSize = 20;
        private IFileObjectRepository fileObjectRepository;
        public HomeController(IFileObjectRepository fileObjectRepository)
        {
            this.fileObjectRepository = fileObjectRepository;
        }

        public ViewResult Index(string tags, string extensions, int page, string tagAndOr)
        {
            ViewData["tags"] = tags;
            ViewData["extensions"] = extensions;
            ViewData["tagAndOr"] = tagAndOr;
            ViewData["currentQuery"] =  (string.IsNullOrEmpty(tags) ? "" : "/tags/" + tags) +  (string.IsNullOrEmpty(extensions) ? "" : "/extensions/" + extensions);
            SearchType TagAndOr = tagAndOr == SearchType.Or.ToString() ? SearchType.Or : SearchType.And;
            ViewData["queryString"] = TagAndOr == SearchType.And ? "" : "?tagsAndOr=Or";

            var filesWithSpecifications = fileObjectRepository.FileObjects.WithTags(tags, TagAndOr).WithExtensions(extensions);
            int numFobs = filesWithSpecifications.Count();
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)numFobs / PageSize);
            
            var display = filesWithSpecifications.OrderBy(x => x.Name).Skip((page - 1) * PageSize).Take(PageSize);
            if (filesWithSpecifications.Count() == 0)
            {
                TempData["warning"] = "no files match your search criteria";
                if (fileObjectRepository.FileObjects.Count() == 0) TempData["warning"] = "no files in forganizer";
            }
            else  TempData["warning"] = (display.Count() == 0) ? "paged too high, click a page below" : (string)null;
            
            return View(display);
        }

        public RedirectToRouteResult Delete(int Id)
        {
            try
            {
                FileObject fileObject = fileObjectRepository.GetFileObject(Id);
                fileObjectRepository.DeleteFileObject(fileObject);
                TempData["success"] = fileObject.FileInfo.Name + " deleted";
            }
            catch (Exception ex) { TempData["error"] = "error: " + ex.Message; }
            return RedirectToAction("Index");
        }
    }
}

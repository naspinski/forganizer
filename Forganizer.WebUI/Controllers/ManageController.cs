using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Forganizer.DomainModel.Entities;
using Forganizer.DomainModel.Abstract;
using System.ComponentModel;

namespace Forganizer.WebUI.Controllers
{
    public class ManageController : Controller
    {
        private IFileObjectRepository fileObjectRepository;
        public ManageController(IFileObjectRepository fileObjectRepository)
        {
            this.fileObjectRepository = fileObjectRepository;
        }

        //public ActionResult Folder()
        //{
        //    return View();
        //}

        //[AcceptVerbs(HttpVerbs.Get)]
        //public ViewResult Folder(int Id)
        //{
        //    //Folder folder = folderRepository.GetFolder(Id);
        //    return View(new Folder());
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Folder(Folder folder)
        {
            TempData["success"] = null;
            if (ModelState.IsValid)
            {
                EntityUtilities.EnsureValid(folder, "Path");
                try { TempData["report"] = folder.AddFilesTo(fileObjectRepository); }
                catch (Exception ex) { TempData["error"] = ex.Message; }
        //        TempData["duplicate"] = EntityUtilities.Categories.EnsureValid(category, categoryRepository, "Name", "ExtensionString");
        //        if (TempData["duplicate"] == null)
        //        {
        //            categoryRepository.SaveCategory(category);
        //            TempData["success"] = category.Name + " has been saved";
        //            return RedirectToAction("Index");
        //        }
                return View(folder);
            }
            else return View(folder);
        }

        public ViewResult AddFolder()
        {
            return View("Folder", new Folder());
        }


        //private static void EnsureValid(IDataErrorInfo validatable, params string[] properties)
        //{
        //    if (properties.Any(x => validatable[x] != null))
        //        throw new InvalidOperationException(validatable.GetType().ToString() + " is invalid");
        //}
    }
}

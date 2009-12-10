using System;
using System.Web.Mvc;
using Forganizer.DomainModel.Abstract;
using Forganizer.DomainModel.Entities;
using Naspinski.MVC.Performance;

namespace Forganizer.WebUI.Controllers
{
    [HandleError]
    [EnableCompression]
    public class CategoryController : Controller
    {
        private ICategoryRepository categoryRepository;
        private IFileObjectRepository fileObjectRepository;
        public CategoryController(ICategoryRepository categoryRepository, IFileObjectRepository fileObjectRepository)
        {
            this.categoryRepository = categoryRepository;
            this.fileObjectRepository = fileObjectRepository;
        }

        [NonAction]
        private void CategoryIDError()
        {
            TempData["error"] = "invalid category id specified, please use the navigation provided";
        }

        public ViewResult Index()
        {
            return View(categoryRepository.Categories);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Edit(int Id)
        {
            Category category = categoryRepository.GetCategory(Id);
            if (!category.IsValid) CategoryIDError();
            return View(category);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                TempData["duplicate"] = EntityUtilities.Categories.EnsureValid(category, categoryRepository, "Name", "ExtensionString");
                if (TempData["duplicate"] == null)
                {                    
                    categoryRepository.SaveCategory(category);
                    TempData["success"] = category.Name + " has been saved";
                    return RedirectToAction("Index");
                }
                else return View(category);
            }
            else return View(category);
        }

        public ViewResult Create()
        {
            return View("Edit", new Category() { IsValid = true });
        }

        public ActionResult Delete(int Id)
        {
            Category category = categoryRepository.GetCategory(Id);
            if (!category.IsValid) CategoryIDError();
            else
            {
                categoryRepository.DeleteCategory(category);
                TempData["success"] = category.Name + " deleted";
            }
            return RedirectToRoute(new { controller = "Category", action = "Index" });
        }

        public RedirectToRouteResult DeleteExtension(int Id, string extension)
        {
            try
            {
                Category category = categoryRepository.GetCategory(Id);
                if (!category.IsValid) CategoryIDError();
                else
                {
                    category.DeleteExtensions(extension);
                    categoryRepository.SaveCategory(category);
                    TempData["success"] = "extension " + extension + " deleted";
                }
            }
            catch (Exception ex) { TempData["error"] = ex.Message; }
            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(bool formPost)
        {
            try
            {
                foreach (string textBoxName in Request.Form.Keys)
                {
                    if (!(string.IsNullOrEmpty(Request.Form[textBoxName].Trim())))
                    {
                        Category category = categoryRepository.GetCategory(Convert.ToInt32(textBoxName.Replace("AddExtensionsTo", "")));
                        try { category.AddExtensions(Request.Form[textBoxName]); }
                        catch (Exception ex) { TempData["warning"] = ex.Message; }
                        categoryRepository.SaveCategory(category);
                    }
                }
                TempData["success"] = "tags added";
            }
            catch (Exception ex) { TempData["error"] = ex.Message; }
            return Index();
        }
    }
}

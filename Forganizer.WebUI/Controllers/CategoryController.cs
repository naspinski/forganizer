using System;
using System.Web.Mvc;
using Forganizer.DomainModel.Abstract;
using Forganizer.DomainModel.Entities;

namespace Forganizer.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository categoryRepository;
        private IFileObjectRepository fileObjectRepository;
        public CategoryController(ICategoryRepository categoryRepository, IFileObjectRepository fileObjectRepository)
        {
            this.categoryRepository = categoryRepository;
            this.fileObjectRepository = fileObjectRepository;
        }

        public ViewResult Index()
        {
            return View(categoryRepository.Categories);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Edit(int Id)
        {
            Category category = categoryRepository.GetCategory(Id);
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
            return View("Edit", new Category());
        }

        public RedirectToRouteResult Delete(int Id)
        {
            Category category = categoryRepository.GetCategory(Id);
            categoryRepository.DeleteCategory(category);
            TempData["success"] = category.Name + " deleted";
            return RedirectToAction("Index");
        }

        public void DeleteExtension(int Id, string extension, string returnUrl)
        {
            try
            {
                Category category = categoryRepository.GetCategory(Id);
                category.DeleteExtensions(extension);
                categoryRepository.SaveCategory(category);
                TempData["success"] = "extension " + extension + " deleted";
            }
            catch (Exception ex) { TempData["error"] = ex.Message; }
            Response.Redirect(returnUrl);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void Index(string returnUrl)
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
            Response.Redirect(returnUrl);
        }
    }
}

using System.Web.Mvc;
using Forganizer.DomainModel.Abstract;
using System.Collections.Generic;
using Forganizer.DomainModel.Entities;
using Forganizer.DomainModel.Extensions;
using System.Linq;

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
    }
}

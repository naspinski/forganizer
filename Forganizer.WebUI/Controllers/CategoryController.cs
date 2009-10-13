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
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public ViewResult Index()
        {
            return View(categoryRepository.Categories);
        }

        public ViewResult Box(string tags, string extensions, string tagAndOr, IEnumerable<FileObject> fileObjects)
        {
            ViewData["tags"] = tags;
            ViewData["extensions"] = extensions;
            ViewData["tagAndOr"] = tagAndOr;

            return View(fileObjects.AsQueryable().Categories(categoryRepository.Categories.Where(x => x.ExtensionString.Length > 0)));
        }

        public ViewResult BoxPart(string tags, string extensions, string tagAndOr, IEnumerable<FileObject> fileObjects, 
            IEnumerable<Tag> Model, bool ActivePart)
        {
            ViewData["tags"] = tags;
            ViewData["extensions"] = extensions;
            ViewData["tagAndOr"] = tagAndOr;
            IEnumerable<string> extensionsInFileObjects = fileObjects.AsQueryable().WithExtensions(extensions).Select(x => x.FileInfo.Extension);
            IEnumerable<Tag> activeCategories = Model.Where(x => x.QueryStringTags.Any(y => extensionsInFileObjects.Contains(y)));
            if (ActivePart) return View(activeCategories);
            else
            {
                IEnumerable<Category> inactiveCategories = categoryRepository.Categories.Where(x => !activeCategories.Select(y => y.Name).Contains(x.Name));
                IEnumerable<Tag> inactiveCategoryTags = inactiveCategories.Select(x => new Tag() { Name = x.Name, QueryString = x.ExtensionString, Size = 1 });
                return View(inactiveCategoryTags);
            }
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

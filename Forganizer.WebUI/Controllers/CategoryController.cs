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

        public ViewResult Box(string tags, string extensions, string tagAndOr, IEnumerable<FileObject> fileObjects)
        {
            ViewData["tags"] = tags;
            ViewData["extensions"] = extensions;
            ViewData["tagAndOr"] = tagAndOr;

            return View(fileObjects.AsQueryable().Categories(categoryRepository.Categories));
        }

        public ViewResult BoxPart(string tags, string extensions, string tagAndOr, IEnumerable<FileObject> fileObjects, 
            IEnumerable<Tag> Model, bool ActivePart)
        {
            ViewData["tags"] = tags;
            ViewData["extensions"] = extensions;
            ViewData["tagAndOr"] = tagAndOr;
            if (ActivePart) return View(Model);
            else
            {
                IEnumerable<string> activeCategoryNames = Model.Select(x => x.Name);
                return View(((IQueryable<FileObject>)null).Categories(categoryRepository.Categories).Where(x => !activeCategoryNames.Contains(x.Name)));
            }
        }       
    }
}

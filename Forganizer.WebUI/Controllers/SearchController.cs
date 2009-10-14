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

namespace Forganizer.WebUI.Controllers
{
    public class SearchController : Controller
    {
        private int PageSize = 15;
        private ICategoryRepository categoryRepository; 
        private IFileObjectRepository fileObjectRepository;
        public SearchController(IFileObjectRepository fileObjectRepository, ICategoryRepository categoryRepository)
        {
            this.fileObjectRepository = fileObjectRepository;
            this.categoryRepository = categoryRepository;
        }

        public ViewResult Index(string tags, string extensions, int page, string tagAndOr)
        {
            FileAndTagCollection fileAndTagCollection = new FileAndTagCollection();
            ViewData["tags"] = tags;
            ViewData["extensions"] = extensions;
            ViewData["currentQuery"] = (string.IsNullOrEmpty(tags) ? "" : "/tags/" + tags) + (string.IsNullOrEmpty(extensions) ? "" : "/extensions/" + extensions);
            SearchType TagAndOr = tagAndOr == SearchType.Or.ToString() ? SearchType.Or : SearchType.And;
            ViewData["tagAndOr"] = TagAndOr.ToString();
            ViewData["queryString"] = "?TagAndOr=" + TagAndOr.ToString();

            //get files with specifications met
            IQueryable<FileObject> filesWithSpecifications = fileObjectRepository.FileObjects.WithTags(tags, TagAndOr).WithExtensions(extensions);
            int numFobs = filesWithSpecifications.Count();
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)numFobs / PageSize);
            
            //set the files to show on the current page
            fileAndTagCollection.PageOfFileObjects = filesWithSpecifications.OrderBy(x => x.Name).Skip((page - 1) * PageSize).Take(PageSize);
            if (filesWithSpecifications.Count() == 0)
            {
                TempData["warning"] = "no files match your search criteria";
                if (fileObjectRepository.FileObjects.Count() == 0) TempData["warning"] = "no files in forganizer";
            }
            else TempData["warning"] = (fileAndTagCollection.PageOfFileObjects.Count() == 0) ? "paged too high, click a page below" : (string)null;

            IQueryable<FileObject> filesWithoutSpecifications = fileObjectRepository.FileObjects.Except(filesWithSpecifications);

            //set the tags
            IEnumerable<Tag> tagCollection = filesWithSpecifications.Tags(true);
            if(TagAndOr == SearchType.Or)
                tagCollection = tagCollection.Concat(filesWithoutSpecifications.Tags(false));
            IEnumerable<Tag> tagsInSearchNotInFiles = tags.SplitTags().Except(tagCollection.Select(x => x.Name))
                .Select(x => new Tag() { Name=x, Count=0, Size=Constants.TagMinSize, Active=false, QueryString=x});
            fileAndTagCollection.Tags = tagCollection.Concat(tagsInSearchNotInFiles);

            //set the extensions
            IEnumerable<Tag> extensionCollection = filesWithSpecifications.Extensions(true);
            extensionCollection = extensionCollection.Concat(filesWithoutSpecifications.Extensions(false));
            IEnumerable<Tag> extensionsInSearchNotInFiles = extensions.SplitTags().Except(extensionCollection.Select(x => x.Name))
                .Select(x => new Tag() { Name=x, Count=0, Size=Constants.TagMinSize, Active=false, QueryString=x});
            fileAndTagCollection.Extensions = extensionCollection.Concat(extensionsInSearchNotInFiles);

            //set the categories
            IEnumerable<Category> categories = categoryRepository.Categories.Where(x => x.ExtensionString.Length > 0);
            fileAndTagCollection.Categories = categories.GetCategoryTags(fileAndTagCollection.Extensions);

            return View(fileAndTagCollection);
        }

        public void Delete(int Id, string returnUrl)
        {
            try
            {
                FileObject fileObject = fileObjectRepository.GetFileObject(Id);
                fileObjectRepository.DeleteFileObject(fileObject);
                TempData["success"] = fileObject.FileInfo.Name + " deleted";
            }
            catch (Exception ex) { TempData["error"] = "error: " + ex.Message; }
            Response.Redirect(returnUrl);
            //return RedirectToAction("Index", new { returnUrl });
        }
    }
}

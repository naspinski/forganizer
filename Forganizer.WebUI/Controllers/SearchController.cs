using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Forganizer.DomainModel;
using Forganizer.DomainModel.Abstract;
using Forganizer.DomainModel.Entities;
using Forganizer.DomainModel.Extensions;
using Forganizer.WebUI.Models;
using Naspinski.MVC.Performance;

namespace Forganizer.WebUI.Controllers
{
    [HandleError]
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

        public ViewResult Index(SearchType tagAndOr, string tags, string extensions, int page)
        {
            FileAndTagCollection fileAndTagCollection = new FileAndTagCollection() { RouteData = this.RouteData };
            
            ViewData["currentQuery"] = (string.IsNullOrEmpty(tags) ? "" : "/tags/" + tags) + (string.IsNullOrEmpty(extensions) ? "" : "/extensions/" + extensions);

            //get files with specifications met
            IQueryable<FileObject> filesWithSpecifications = fileObjectRepository.FileObjects.WithTags(tags, tagAndOr).WithExtensions(extensions);
            int numFobs = filesWithSpecifications.Count();
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
            if(tagAndOr == SearchType.Or)
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

        [EnableCompression]
        public RedirectResult Delete(int Id, string returnTo)
        {
            try
            {
                FileObject fileObject = fileObjectRepository.GetFileObject(Id);
                fileObjectRepository.DeleteFileObject(fileObject);
                fileObjectRepository.SubmitChanges();
                TempData["success"] = fileObject.FileInfo.Name + " deleted";
            }
            catch (Exception ex) { TempData["error"] = "error: " + ex.Message; }
            return Redirect("~/" + returnTo);
        }

        [EnableCompression]
        public RedirectResult DeleteTag(int Id, string tag, string returnTo)
        {
            try
            {
                FileObject fileObject = fileObjectRepository.GetFileObject(Id);
                fileObject.DeleteTags(tag);
                fileObjectRepository.SaveFileObject(fileObject);
                fileObjectRepository.SubmitChanges();
                TempData["success"] = "tag " + tag + " deleted";
            }
            catch (Exception ex) { TempData["error"] = ex.Message; }
            return Redirect("~/" + returnTo);
        }

        [EnableCompression]
        [AcceptVerbs(HttpVerbs.Post)]
        public RedirectResult AddTags(string returnTo)
        {
            try
            {
                foreach (string textBoxName in Request.Form.Keys)
                {
                    if (!(string.IsNullOrEmpty(Request.Form[textBoxName].Trim())))
                    {
                        FileObject fileObject = fileObjectRepository.GetFileObject(Convert.ToInt32(textBoxName.Replace("AddTagsTo", "")));
                        try { fileObject.AddTags(Request.Form[textBoxName]); }
                        catch (Exception ex) { TempData["warning"] = ex.Message; }
                        fileObjectRepository.SaveFileObject(fileObject);
                    }
                }
                fileObjectRepository.SubmitChanges();
                TempData["success"] = "tags added";
            }
            catch (Exception ex) { TempData["error"] = ex.Message; }
            return Redirect("~" + returnTo);
        }

        [EnableCompression]
        public FileResult Download(string filePath)
        {
            return File(filePath, "application/octetstream", Path.GetFileName(filePath));
        }
    }
}

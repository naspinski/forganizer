using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Forganizer.DomainModel.Entities;
using Forganizer.DomainModel.Extensions;
using Forganizer.DomainModel.Abstract;
using System.ComponentModel;
using Forganizer.DomainModel;
using Forganizer.WebUI.Models;

namespace Forganizer.WebUI.Controllers
{
    public class ManageController : Controller
    {
        private IFileObjectRepository fileObjectRepository;
        public ManageController(IFileObjectRepository fileObjectRepository)
        {
            this.fileObjectRepository = fileObjectRepository;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Folder(Folder folder)
        {
            if (ModelState.IsValid)
            {
                EntityUtilities.EnsureValid(folder, "Path");
                try { TempData["report"] = folder.AddFilesTo(fileObjectRepository); }
                catch (Exception ex) { TempData["error"] = ex.Message; }
                return View(folder);
            }
            else return View(folder);
        }

        public ViewResult AddFolder()
        {
            return View("Folder", new Folder());
        }

        public ViewResult Cleanup()
        {
            return View(new List<string>());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Cleanup(IEnumerable<string> deleted)
        {
            try
            {
                deleted = fileObjectRepository.Cleanup();
                fileObjectRepository.SubmitChanges();
                if (deleted.Count() == 0) deleted = new List<string> { "nothing to clean up" };
            }
            catch (Exception ex) { TempData["error"] = ex.Message; }
            return View(deleted);
        }

        public ViewResult FormRoot()
        {
            return View();
        }

        public ViewResult Tags(string tagEditType)
        {
            TagEditModel tagEditModel = new TagEditModel();
            SetTagEditType(tagEditModel, tagEditType);
            return View(tagEditModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Tags(TagEditModel tagEditModel, string tagEditType)
        {
            SetTagEditType(tagEditModel, tagEditType);
            List<string> report = new List<string>();
            if(ModelState.IsValid)
            {
                tagEditModel.Replace = tagEditModel.Replace ?? "";
                tagEditModel.With = tagEditModel.With ?? "";
                IEnumerable<int> fileObjectIds = GetRelevantFileObjectIds(tagEditModel);

                foreach (int Id in fileObjectIds)
                {
                    FileObject fileObject = fileObjectRepository.GetFileObject(Id);
                    fileObject.ReplaceTags(tagEditModel.Replace, tagEditModel.With);
                    fileObjectRepository.SaveFileObject(fileObject);
                    if (fileObject.Active) report.Add(fileObject.Name + " tags updated");
                }
                fileObjectRepository.SubmitChanges();
                if (report.Count == 0) report.Add("no active files matched your criteria");     
                TempData["report"] = report;
            }
            return View(tagEditModel);
        }

        private void SetTagEditType(TagEditModel tagEditModel, string tagEditType)
        {
            switch (tagEditType)
            {
                case "Add": tagEditModel.EditType = TagEditType.Add; break;
                case "Delete": tagEditModel.EditType = TagEditType.Delete; break;
                default: tagEditModel.EditType = TagEditType.Edit; break;
            }
        }

        private IEnumerable<int> GetRelevantFileObjectIds(TagEditModel tagEditModel)
        {
            int test = "ASDF".LastIndexOf("\\", 2);
            List<int> Ids = new List<int>();
            int distanceFromStartForSlashCheck = tagEditModel.Path.EndsWith("\\") ? tagEditModel.Path.Length : tagEditModel.Path.Length + 2;
            
            var filesWithPathTagsAndIncludedExtensionsInProperFolders = fileObjectRepository.AllFileObjects.Where(x => x.FilePath.StartsWith(tagEditModel.Path)
                && ((tagEditModel.Recursive || string.IsNullOrEmpty(tagEditModel.Path)) ? true // this part checks if it is recursive
                    // makes sure there isn't a slash after the root is removed... meaning it's in the same folder
                    : (x.FilePath.Length > distanceFromStartForSlashCheck && !x.FilePath.Substring(distanceFromStartForSlashCheck).Contains("\\")) 
                ))
                .WithTags(tagEditModel.Replace, SearchType.Or).WithExtensions(tagEditModel.Include)
                .Select(x => new { x.Id, x.FilePath });

            if (tagEditModel.ExcludeExtensions.Count() > 0) // no efficient way I could find to add this up above
            {
                foreach (var fileObject in filesWithPathTagsAndIncludedExtensionsInProperFolders)
                    if (tagEditModel.ExcludeExtensions.All(x => !fileObject.FilePath.EndsWith(x))) Ids.Add(fileObject.Id);
            }
            else Ids = filesWithPathTagsAndIncludedExtensionsInProperFolders.Select(x => x.Id).ToList();
            return Ids;
        }
    }
}

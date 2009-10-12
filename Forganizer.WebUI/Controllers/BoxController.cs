using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Forganizer.DomainModel.Abstract;
using Forganizer.DomainModel.Entities;
using Forganizer.DomainModel.Extensions;
using Forganizer.DomainModel;

namespace Forganizer.WebUI.Controllers
{
    public class BoxController : Controller
    {
        private IFileObjectRepository fileObjectRepository;
        public BoxController(IFileObjectRepository fileObjectRepository)
        {
            this.fileObjectRepository = fileObjectRepository;
        }

        public ViewResult TagCloud(string tags, string extensions, string tagAndOr)
        {
            ViewData["tags"] = tags;
            ViewData["extensions"] = extensions;
            ViewData["tagAndOr"] = tagAndOr;
            SearchType TagAndOr = tagAndOr == SearchType.Or.ToString() ? SearchType.Or : SearchType.And;
            return View(fileObjectRepository.FileObjects.WithTags(tags, TagAndOr).WithExtensions(extensions).Tags());
        }

        public ViewResult TagCloudPart(string tags, string extensions, string tagAndOr, IEnumerable<Tag> Model, bool ActivePart)
        {
            ViewData["tags"] = tags;
            ViewData["extensions"] = extensions;
            ViewData["tagAndOr"] = tagAndOr;
            SearchType TagAndOr = tagAndOr == SearchType.Or.ToString() ? SearchType.Or : SearchType.And;
            if (ActivePart) return View(Model);
            else
            {
                if (TagAndOr == SearchType.And) return View();
                else 
                {
                    IEnumerable<string> activeTagNames = Model.Select(x => x.Name);
                    return View(fileObjectRepository.FileObjects.WithExtensions(extensions).Tags().Where(x => !activeTagNames.Contains(x.Name)));
                }
            }
        }

        public ViewResult ExtensionCloud(string tags, string extensions, string tagAndOr)
        {
            ViewData["tags"] = tags;
            ViewData["extensions"] = extensions;
            ViewData["tagAndOr"] = tagAndOr;
            if (ViewData["tags"] == null) ViewData["tags"] = "";
            if (ViewData["extensions"] == null) ViewData["extensions"] = "";
            SearchType TagAndOr = tagAndOr == SearchType.Or.ToString() ? SearchType.Or : SearchType.And;

            IEnumerable<Tag> allExtensionsInQuery = extensions.SplitTags().Select(x => new Tag() { Name = x, Count = 1, Size = Constants.TagMinSize });
            IEnumerable<Tag> display = fileObjectRepository.FileObjects.WithTags(tags, TagAndOr).WithExtensions(extensions).Extensions();
            IEnumerable<Tag> extensionsInQueryNotInFile = allExtensionsInQuery.Where(x => !display.Select(y => y.Name).Contains(x.Name));

            return View(display.Union(extensionsInQueryNotInFile));
        }

        public ViewResult ExtensionCloudPart(string tags, string extensions, string tagAndOr, IEnumerable<Tag> Model, bool ActivePart)
        {
            ViewData["tags"] = tags;
            ViewData["extensions"] = extensions;
            ViewData["tagAndOr"] = tagAndOr;
            SearchType TagAndOr = tagAndOr == SearchType.Or.ToString() ? SearchType.Or : SearchType.And;
            if (ActivePart) return View(Model);
            else
            {
                IEnumerable<string> activeExtensionNames = Model.Select(x => x.Name);
                return View(fileObjectRepository.FileObjects.WithTags(tags, TagAndOr).Extensions().Where(x => !activeExtensionNames.Contains(x.Name)));
            }
        }
    }
}

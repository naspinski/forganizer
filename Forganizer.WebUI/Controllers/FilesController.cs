using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Forganizer.DomainModel.Entities;
using Forganizer.DomainModel.Extensions;
using Forganizer.DomainModel.Abstract;

namespace Forganizer.WebUI.Controllers
{
    public class FilesController : Controller
    {
        private IFileObjectRepository fileObjectRepository;
        public FilesController(IFileObjectRepository fileObjectRepository)
        {
            this.fileObjectRepository = fileObjectRepository;
        }


        public ViewResult TagCloud()
        {
            var tags = fileObjectRepository.FileObjects.Tags();
            ViewData["tagTotal"] = tags.Select(x => x.Count).Sum();
            return View(tags);
        }

        public ViewResult ExtensionCloud()
        {
            return View(fileObjectRepository.FileObjects.Extensions());
        }

        public ViewResult CategoryCloud()
        {
            return View(fileObjectRepository.FileObjects);
        }

        public ViewResult Recent(int count)
        {
            return View(fileObjectRepository.FileObjects.OrderByDescending(x => x.Modified).Take(count));
        }
    }
}

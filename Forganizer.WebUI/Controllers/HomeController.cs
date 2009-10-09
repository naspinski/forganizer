using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Forganizer.DomainModel.Abstract;

namespace Forganizer.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IFileObjectRepository fileObjectRepository;
        public HomeController(IFileObjectRepository fileObjectRepository)
        {
            this.fileObjectRepository = fileObjectRepository;
        }

        public ViewResult Dashboard()
        {
            var fileObjects = fileObjectRepository.FileObjects.OrderByDescending(x => x.Modified).Take(20);
            //return View(fileObjectRepository.FileObjects.OrderByDescending(x => x.Modified).Take(20));
            return View(fileObjects);
        }
    }
}

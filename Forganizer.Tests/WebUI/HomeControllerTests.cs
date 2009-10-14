using System.Collections.Generic;
using Forganizer.DomainModel.Abstract;
using Forganizer.DomainModel.Entities;
using Forganizer.DomainModel.Concrete;
using Forganizer.WebUI.Controllers;
using NUnit.Framework;
using System.Linq;
using System.Web.Mvc;
using Moq;

namespace Forganizer.Tests.WebUI
{
    [TestFixture]
    class HomeControllerTests
    {
        Mock<IFileObjectRepository> mockFileObjectRepository = Utilities.Mocking.MockFileObjectRepository();
        
        [Test]
        public void Index_presents_most_recently_modified_fileObjects()
        {
            //HomeController controller = new HomeController(mockFileObjectRepository.Object);

            //ViewResult result = controller.Index("funny", "", 1, "And");
            //Assert.IsNotNull(result, "did not render view");
            //IEnumerable<FileObject> fileObjects = result.ViewData.Model as IEnumerable<FileObject>;
            //Assert.AreEqual(2, fileObjects.Count());
            //Assert.AreEqual(".jpg", fileObjects.First().FileInfo.Extension);

            //result = controller.Index("funny|fake", "", 1, "And");
            //fileObjects = result.ViewData.Model as IEnumerable<FileObject>;
            //Assert.AreEqual(0, fileObjects.Count());

            //result = controller.Index("funny", ".txt", 1, "And");
            //fileObjects = result.ViewData.Model as IEnumerable<FileObject>;
            //Assert.AreEqual(1, fileObjects.Count());
        }

        [Test]
        public void Delete_works()
        {
            //HomeController controller = new HomeController(mockFileObjectRepository.Object);
            //FileObject fileObject = mockFileObjectRepository.Object.FileObjects.First(x => x.Id == 1);

            //Assert.AreEqual(4, mockFileObjectRepository.Object.AllFileObjects.Count());
            //Assert.AreEqual(3, mockFileObjectRepository.Object.FileObjects.Count());
            //RedirectToRouteResult result = controller.Delete(1);
            //Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}

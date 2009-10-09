using System.Collections.Generic;
using Forganizer.DomainModel.Abstract;
using Forganizer.DomainModel.Entities;
using Forganizer.WebUI.Controllers;
using NUnit.Framework;
using System.Linq;

namespace Forganizer.Tests.WebUI
{
    [TestFixture]
    class HomeControllerTests
    {
        [Test]
        public void Dashboard_presents_most_recently_modified_fileObjects()
        {
            IFileObjectRepository repository = Utilities.Mocking.MockFileObjectRepository();
            HomeController controller = new HomeController(repository);

            var result = controller.Dashboard();

            Assert.IsNotNull(result, "did not render view");
            var fileObjects = result.ViewData.Model as IQueryable<FileObject>;
            Assert.AreEqual("another_image.jpg", fileObjects.First().FileInfo().Name);
            Assert.AreEqual(3, fileObjects.Count());
        }
    }
}

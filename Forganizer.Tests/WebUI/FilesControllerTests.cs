using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Forganizer.DomainModel.Abstract;
using Forganizer.WebUI.Controllers;
using Forganizer.DomainModel.Entities;

namespace Forganizer.Tests.WebUI
{
    [TestFixture]
    class FilesControllerTests
    {
        [Test]
        public void Recent_return_most_recent()
        {
            var repository = Utilities.Mocking.MockFileObjectRepository();
            FilesController controller = new FilesController(repository);

            var result = controller.Recent(2);

            var fileObjects = result.ViewData.Model as IQueryable<FileObject>;
            Assert.AreEqual(2, fileObjects.Count());
            Assert.IsTrue(fileObjects.First().Modified > fileObjects.Last().Modified);
        }

        [Test]
        public void TagCloud_returns_proper_cloud()
        {
            var repository = Utilities.Mocking.MockFileObjectRepository();
            FilesController controller = new FilesController(repository);

            var result = controller.TagCloud();

            var tags = result.ViewData.Model as IEnumerable<Tag>;
            Assert.AreEqual(2, tags.First(x => x.Name == "funny").Count);
            Assert.AreEqual(1, tags.First(x => x.Name == "long").Count);
            Assert.AreEqual(4, tags.Count());
            Assert.AreEqual(6, tags.Select(x => x.Count).Sum());
        }

        [Test]
        public void Extension_cloud_returns_proper_cloud()
        {
            var repository = Utilities.Mocking.MockFileObjectRepository();
            FilesController controller = new FilesController(repository);

            var result = controller.ExtensionCloud();

            var extensions = result.ViewData.Model as IEnumerable<Tag>;
            Assert.AreEqual(2, extensions.First(x => x.Name == ".jpg").Count);
            Assert.AreEqual(2, extensions.Count());
            Assert.AreEqual(3, extensions.Select(x => x.Count).Sum());
        }

        [Test]
        public void CategoryCloud_returns_category_cloud()
        {
            var repository = Utilities.Mocking.MockFileObjectRepository();
            FilesController controller = new FilesController(repository);

            var result = controller.CategoryCloud();
        }
    }
}

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
    class BoxControllerTests
    {
        IFileObjectRepository repository = Utilities.Mocking.MockFileObjectRepository().Object;

        [Test]
        public void TagCloud_returns_proper_cloud()
        {
            BoxController controller = new BoxController(repository);

            var result = controller.TagCloud("","","And");

            var tags = result.ViewData.Model as IEnumerable<Tag>;
            Assert.AreEqual(2, tags.First(x => x.Name == "funny").Count);
            Assert.AreEqual(1, tags.First(x => x.Name == "long").Count);
            Assert.AreEqual(4, tags.Count());
            Assert.AreEqual(6, tags.Select(x => x.Count).Sum());
        }

        [Test]
        public void Extension_cloud_returns_proper_cloud()
        {
            BoxController controller = new BoxController(repository);

            var result = controller.ExtensionCloud("","","And");

            var extensions = result.ViewData.Model as IEnumerable<Tag>;
            Assert.AreEqual(2, extensions.First(x => x.Name == ".jpg").Count);
            Assert.AreEqual(2, extensions.Count());
            Assert.AreEqual(3, extensions.Select(x => x.Count).Sum());
        }
    }
}

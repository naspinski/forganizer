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
        }

        [Test]
        public void Extension_cloud_returns_proper_cloud()
        {
        }
    }
}

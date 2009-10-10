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
            HomeController controller = new HomeController();

            var result = controller.Dashboard();

            Assert.IsNotNull(result, "did not render view");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Forganizer.WebUI.Controllers;
using System.Web.Mvc;
using Moq;
using System.Web;

namespace Forganizer.Tests.WebUI
{
    [TestFixture]
    class ErrorControllerTests
    {
        ErrorController controller;
        Utilities.ContextMocks mocks;
        [SetUp]
        public void Setup()
        {
            controller = new ErrorController();
            mocks = new Utilities.ContextMocks(controller);
        }

        [Test]
        public void NotFound_works_with_both_inputs()
        {
            ViewResult result = controller.NotFound("some/url", "should_be_ignored");
            Assert.AreEqual(result.ViewData["url"], "some/url");
        }

        [Test]
        public void NotFound_works_with_first_input()
        {
            ViewResult result = controller.NotFound("some/url", null);
            Assert.AreEqual(result.ViewData["url"], "some/url");
        }

        [Test]
        public void NotFound_works_with_last_input()
        {
            ViewResult result = controller.NotFound(null, "some/url");
            Assert.AreEqual(result.ViewData["url"], "some/url");
        }

        [Test]
        public void NotFound_works_with_no_input()
        {
            ViewResult result = controller.NotFound(null, null);
            Assert.AreEqual(result.ViewData["url"], null);
        }
    }
}

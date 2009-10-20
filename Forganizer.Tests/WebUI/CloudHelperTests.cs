using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Forganizer.DomainModel.Entities;
using Forganizer.WebUI.HtmlHelpers;
using NUnit.Framework;

namespace Forganizer.Tests.WebUI
{
    [TestFixture]
    class CloudHelperTests
    {
        [Test]
        public void Extends_HtmlHelper()
        {
            HtmlHelper html = null;
            html.Cloud((UrlHelper)null, new List<Tag>(), "", "", "", TagType.Category);
        }

        [Test]
        public void Makes_correct_clouds()
        {
            string cloud = ((HtmlHelper)null).Cloud((UrlHelper)null, new List<Tag>(), "tgs", "exts", "And", TagType.Category);

            Assert.AreEqual(@"<div class=""active"">" + Environment.NewLine, cloud.Trim().Substring(0, 22));
        }
    }
}

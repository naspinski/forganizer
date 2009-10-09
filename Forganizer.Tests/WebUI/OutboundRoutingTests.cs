using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Forganizer.Tests.WebUI
{
    [TestFixture]
    class OutboundRoutingTests
    {
        [Test]
        public void Home()
        {
            Assert.AreEqual("/", Utilities.Routing.GetOutboundUrl(new { controller = "Home", action = "Dashboard" }));
        }

        [Test]
        public void Tags()
        {
            Assert.AreEqual("/tags/abc", Utilities.Routing.GetOutboundUrl(new { controller = "Files", action = "List", tags = "abc" }));
        }

        [Test]
        public void Tags_page()
        {
            Assert.AreEqual("/tags/abc/page101", Utilities.Routing.GetOutboundUrl(new { controller = "Files", action = "List", tags = "abc", page = 101 }));
        }

        [Test]
        public void Tags_extensions()
        {
            Assert.AreEqual("/tags/abc/extensions/def", Utilities.Routing.GetOutboundUrl(new { controller = "Files", action = "List", tags = "abc", extensions = "def" }));
        }

        [Test]
        public void Tags_extensions_page()
        {
            Assert.AreEqual("/tags/abc/extensions/def/page101", Utilities.Routing.GetOutboundUrl(new { controller = "Files", action = "List", tags = "abc", extensions = "def", page = 101 }));
        }

        [Test]
        public void Extensions()
        {
            Assert.AreEqual("/extensions/abc", Utilities.Routing.GetOutboundUrl(new { controller = "Files", action = "List", extensions = "abc" }));
        }

        [Test]
        public void Extensions_page()
        {
            Assert.AreEqual("/extensions/abc/page101", Utilities.Routing.GetOutboundUrl(new { controller = "Files", action = "List", extensions = "abc", page = 101 }));
        }

        [Test]
        public void Extensions_Tags()
        {
            Assert.AreEqual("/tags/abc/extensions/def", Utilities.Routing.GetOutboundUrl(new { controller = "Files", action = "List", tags = "abc", extensions = "def" }));
        }

        [Test]
        public void Extensions_tags_page()
        {
            Assert.AreEqual("/tags/abc/extensions/def/page101", Utilities.Routing.GetOutboundUrl(new { controller = "Files", action = "List", tags = "abc", extensions = "def", page = 101 }));
        }
    }
}

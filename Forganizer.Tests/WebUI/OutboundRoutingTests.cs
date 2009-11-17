using NUnit.Framework;

namespace Forganizer.Tests.WebUI
{
    [TestFixture]
    class OutboundRoutingTests
    {
        [Test]
        public void Home()
        {
            Assert.AreEqual("/", Utilities.Routing.GetOutboundUrl(new { controller = "Search", action = "Index" }));
        }

        [Test]
        public void Tags()
        {
            Assert.AreEqual("/Tags/And/abc", Utilities.Routing.GetOutboundUrl(new { controller = "Search", action = "Index", tags = "abc", tagAndOr="And" }));
        }

        [Test]
        public void Tags_andOr()
        {
            Assert.AreEqual("/Tags/Or/abc", Utilities.Routing.GetOutboundUrl(new { controller = "Search", action = "Index", tags = "abc", tagAndOr="Or" }));
        }

        [Test]
        public void Tags_page()
        {
            Assert.AreEqual("/Tags/abc/Page101", Utilities.Routing.GetOutboundUrl(new { controller = "Search", action = "Index", tags = "abc", page = 101 }));
        }

        [Test]
        public void Tags_andOr_page()
        {
            Assert.AreEqual("/Tags/Or/abc/Page101", Utilities.Routing.GetOutboundUrl(new { controller = "Search", action = "Index", tags = "abc", page = 101, tagAndOr="Or" }));
        }

        [Test]
        public void Tags_extensions()
        {
            Assert.AreEqual("/Tags/abc/Extensions/def", Utilities.Routing.GetOutboundUrl(new { controller = "Search", action = "Index", tags = "abc", extensions = "def" }));
        }

        [Test]
        public void Tags_andOr_extensions()
        {
            Assert.AreEqual("/Tags/Or/abc/Extensions/def", Utilities.Routing.GetOutboundUrl(new { controller = "Search", action = "Index", tags = "abc", extensions = "def", tagAndOr = "Or" }));
        }

        [Test]
        public void Tags_extensions_page()
        {
            Assert.AreEqual("/Tags/abc/Extensions/def/Page101", Utilities.Routing.GetOutboundUrl(new { controller = "Search", action = "Index", tags = "abc", extensions = "def", page = 101 }));
        }

        [Test]
        public void Tags_andOr_extensions_page()
        {
            Assert.AreEqual("/Tags/Or/abc/Extensions/def/Page101", Utilities.Routing.GetOutboundUrl(new { controller = "Search", action = "Index", tags = "abc", extensions = "def", page = 101, tagAndOr = "Or" }));
        }

        [Test]
        public void Extensions()
        {
            Assert.AreEqual("/Extensions/abc", Utilities.Routing.GetOutboundUrl(new { controller = "Search", action = "Index", extensions = "abc" }));
        }

        [Test]
        public void Extensions_page()
        {
            Assert.AreEqual("/Extensions/abc/Page101", Utilities.Routing.GetOutboundUrl(new { controller = "Search", action = "Index", extensions = "abc", page = 101 }));
        }
    }
}

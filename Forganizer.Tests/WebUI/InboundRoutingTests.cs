using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Forganizer.Tests.WebUI
{
    [TestFixture]
    class InboundRoutingTests
    {
        [Test]
        public void Slash_goes_to_Home()
        {
            Utilities.Routing.TestInboundRoute("~/", new { controller = "Home", action = "Dashboard" });
        }

        [Test]
        public void Tags_slash_something()
        {
            Utilities.Routing.TestInboundRoute("~/tags/a|b|c", new { controller = "Files", action = "List", tags = "a|b|c", extensions = "", page = 1 });
        }

        [Test]
        public void Tags_slash_something_slash_pageNumber()
        {
            Utilities.Routing.TestInboundRoute("~/tags/a/page101", new { controller = "Files", action = "List", tags = "a", extensions = "", page = 101 });
        }

        [Test]
        public void Tags_slash_something_slash_extensions_slash_something()
        {
            Utilities.Routing.TestInboundRoute("~/tags/a/extensions/b", new { controller = "Files", action = "List", tags = "a", extensions = "b", page = 1 });
        }

        [Test]
        public void Tags_slash_something_slash_extensions_slash_something_slash_pageNumer()
        {
            Utilities.Routing.TestInboundRoute("~/tags/a/extensions/b/page101", new { controller = "Files", action = "List", tags = "a", extensions = "b", page = 101 });
        }

        [Test]
        public void Extensions_slash_something()
        {
            Utilities.Routing.TestInboundRoute("~/extensions/b", new { controller = "Files", action = "List", tags = "", extensions = "b", page = 1 });
        }

        [Test]
        public void Extensions_slash_something_slash_pageNumber()
        {
            Utilities.Routing.TestInboundRoute("~/extensions/b/page101", new { controller = "Files", action = "List", tags = "", extensions = "b", page = 101 });
        }

        [Test]
        public void Extensions_slash_something_slash_tags_slash_something()
        {
            Utilities.Routing.TestInboundRoute("~/extensions/b/tags/a", new { controller = "Files", action = "List", tags = "a", extensions = "b", page = 1 });
        }

        [Test]
        public void Extensions_slash_something_slash_tags_slash_something_slash_pageNumer()
        {
            Utilities.Routing.TestInboundRoute("~/extensions/b/tags/a/page101", new { controller = "Files", action = "List", tags = "a", extensions = "b", page = 101 });
        }
    }
}

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
            Utilities.Routing.TestInboundRoute("~/", new { controller = "Search", action = "Index" });
        }

        [Test]
        public void Tags_slash_something()
        {
            Utilities.Routing.TestInboundRoute("~/Tags/a|b|c", new { controller = "Search", action = "Index", tags = "a|b|c", extensions = "", page = 1 });
        }

        [Test]
        public void Tags_slash_something_slash_pageNumber()
        {
            Utilities.Routing.TestInboundRoute("~/Tags/a/page101", new { controller = "Search", action = "Index", tags = "a", extensions = "", page = 101 });
        }

        [Test]
        public void Tags_slash_something_slash_extensions_slash_something()
        {
            Utilities.Routing.TestInboundRoute("~/Tags/a/Extensions/b", new { controller = "Search", action = "Index", tags = "a", extensions = "b", page = 1 });
        }

        [Test]
        public void Tags_slash_something_slash_extensions_slash_something_slash_pageNumer()
        {
            Utilities.Routing.TestInboundRoute("~/Tags/a/Extensions/b/page101", new { controller = "Search", action = "Index", tags = "a", extensions = "b", page = 101 });
        }

        [Test]
        public void Extensions_slash_something()
        {
            Utilities.Routing.TestInboundRoute("~/Extensions/b", new { controller = "Search", action = "Index", tags = "", extensions = "b", page = 1 });
        }

        [Test]
        public void Extensions_slash_something_slash_pageNumber()
        {
            Utilities.Routing.TestInboundRoute("~/Extensions/b/Page101", new { controller = "Search", action = "Index", tags = "", extensions = "b", page = 101 });
        }
    }
}

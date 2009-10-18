using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forganizer.WebUI.HtmlHelpers
{
    public class NavLink
    {
        public string Text { get; set; }
        public string Controller { get; set; }
        public string View { get; set; }
        public string[] Views { get; set; }
        public string RouteString { get; set; }
    }
}

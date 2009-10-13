using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using Microsoft.Web.Mvc.Controls;

namespace Forganizer.WebUI.HtmlHelpers
{
    public static class NavHelper
    {
        public enum MenuType { MainNavigation, Footer }
        public static string NavMenu(this HtmlHelper html, UrlHelper urlHelper, MenuType menuType)
        {
            NavLink[] links = {
                new NavLink() { Text = "home", Controller="Home", View="Index" },
                new NavLink() { Text = "categories", Controller = "Category", View = "Index" },
                new NavLink() { Text = "add files", Controller = "Files", View = "Index" } };
            string[] url = HttpContext.Current.Request.Url.Segments;
           
            string navMenu = string.Empty;
            bool isHome = !links.Select(x => x.Controller).Any(x => url.Contains(x + "/"));

            for (int i = 0; i < links.Length; i++)
            {
                string lnk = "<a " + ((isHome && links[i].Controller == "Home") || (!isHome && url.Contains(links[i].Controller + "/")) ? "class=\"selected\" " : "") +
                    "href=\"" + urlHelper.Action(links[i].View, links[i].Controller).ToString() + "\">" + links[i].Text + "</a>";
                if (menuType == MenuType.MainNavigation) navMenu += "<li>" + lnk + "</li>";
                else if (menuType == MenuType.Footer) navMenu += (i == 0 ? "" : " | ") + lnk;
            }
            return navMenu;
        }

        private class NavLink
        {
            public string Text { get; set; }
            public string Controller { get; set; }
            public string View { get; set; }
        }
    }
}

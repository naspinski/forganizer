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
        public static string NavMenu(this HtmlHelper html, UrlHelper urlHelper, RouteValueDictionary routeValues, MenuType menuType)
        {
            NavLink[] links = {
                new NavLink() { Text = "search", Controller="Search", View="Index" },
                new NavLink() { Text = "categories", Controller = "Category", View = "Index" },
                new NavLink() { Text = "manage", Controller = "Manage", View = "AddFolder" } };
           
            string navMenu = string.Empty;

            for (int i = 0; i < links.Length; i++)
            {
                bool isSelected = routeValues["controller"].ToString() == links[i].Controller;
                string lnk = "<a " + (isSelected ? "class=\"selected\" " : "") +
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

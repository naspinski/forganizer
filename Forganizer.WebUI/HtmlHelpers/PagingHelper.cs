using System;
using System.Text;
using System.Web.Mvc;

namespace Forganizer.WebUI.HtmlHelpers
{
    public static class PagingHelper
    {
        public static string PageLinks(this HtmlHelper html, int currentPage, int totalPages, Func<int, string> pageUrl)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= totalPages; i++)
                sb.Append("<a " + (currentPage == i ? "class=\"selected\" " : "") + "href=\"" + pageUrl(i) + "\">" + i + "</a>");
            return sb.ToString();
        }
    }
}

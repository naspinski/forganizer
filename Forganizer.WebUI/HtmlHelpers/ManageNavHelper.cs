using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Forganizer.WebUI.HtmlHelpers
{
    public static class ManageNavHelper
    {
        public static string ManageMenu(this HtmlHelper html, UrlHelper urlHelper, RouteValueDictionary routeValues)
        {
            StringBuilder sb = new StringBuilder();
            string url;
            bool isSelected;

            NavLink[] links = {
                new NavLink() { Text = "add files", Controller="Manage", View="AddFolder", Views = new string[] {"AddFolder","Folder"}, RouteString = string.Empty },
                new NavLink() { Text = "delete files", Controller="Manage", View="Delete", Views = new string[] {"Delete"}, RouteString = string.Empty },
                new NavLink() { Text = "file cleanup", Controller="Manage", View="Cleanup", Views = new string[] {"Cleanup"}, RouteString=string.Empty },
                new NavLink() { Text = "mass tag editing", Controller = "Manage", View = "Tags", Views = new string[] {"Tags"}, RouteString="Edit" },
                new NavLink() { Text = "mass tag deleting", Controller = "Manage", View = "Tags", Views = new string[] {"Tags"}, RouteString = "Delete" },
                new NavLink() { Text = "mass tag adding", Controller = "Manage", View = "Tags", Views = new string[] {"Tags"}, RouteString = "Add" },
            };

            sb.AppendLine("        <fieldset class=\"box\">");
            sb.AppendLine("            <legend>actions</legend>");
            sb.AppendLine("            <ul class=\"list_vertical\">");
            foreach(NavLink link in links)
            {

                url = urlHelper.Action(link.View, link.Controller,
                    (string.IsNullOrEmpty(link.RouteString) ? null : new { tagEditType = link.RouteString })).ToString();

                if (link.View == "Tags" && routeValues["tagEditType"] != null)
                    isSelected = link.RouteString == routeValues["tagEditType"].ToString();
                else
                    isSelected = link.Views.Contains(routeValues["action"]);

                sb.AppendLine("                <li><a  href=\"" + url + "\">" + (isSelected ? "&raquo; " : "") + link.Text + "</a></li>");
            }
            sb.AppendLine("            </ul>");
            sb.AppendLine("        </fieldset>");
            return sb.ToString();
        }
    }
}

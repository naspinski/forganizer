using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Forganizer.DomainModel.Entities;
using Forganizer.DomainModel.Extensions;

namespace Forganizer.WebUI.HtmlHelpers
{
    public static class CloudHelper
    {
        public static string Cloud(this HtmlHelper html, UrlHelper url, IEnumerable<Tag> allTags, string tags, string extensions, string tagAndOr, TagType tagType)
        {
            bool[] bools = { true, false };
            StringBuilder sb = new StringBuilder();

            foreach (bool isActive in bools)
            {
                sb.AppendLine("            <div class=\"" + (isActive ? "active" : "inactive") + "\">");
                sb.AppendLine("                <ul class=\"list_horizontal\">");
                foreach (Tag tag in allTags.Where(x => x.Active == isActive))
                {
                    sb.AppendLine("                    <li><a style=\"font-size:" + tag.Size.ToString("0.0") + "em;\" href=\"" +
                        url.Action("Index", "Search", new
                        {
                            TagAndOr = tagAndOr,
                            tags = (tagType == TagType.Tag ? tags.AddToSearch(tag.QueryString) : tags),
                            extensions = (tagType == TagType.Tag ? extensions : extensions.AddToSearch(tag.QueryString))
                        }).ToString() + "\">" +
                        tag.Name + "</a>");

                    if ((tagType == TagType.Tag && tags.SplitTags().Contains(tag.Name)) ||
                            (tagType == TagType.Extension && extensions.SplitTags().Contains(tag.Name)) ||
                            (tagType == TagType.Category && extensions.SplitTags().Intersect(tag.QueryStringTags).Count() > 0))
                    {
                        sb.AppendLine("                    <a class=\"x\" href=\"" +
                            url.Action("Index", "Search", new
                            {
                                TagAndOr = tagAndOr,
                                tags = (tagType == TagType.Tag ? tags.RemoveFromSearch(tag.QueryString) : tags),
                                extensions = (tagType == TagType.Tag ? extensions : extensions.RemoveFromSearch(tag.QueryString))
                            }).ToString() +
                            "\">x</a></li>");
                    }
                }
                sb.AppendLine("                </ul>");
                sb.AppendLine("            </div>");
            }
            return sb.ToString();
        }
    }
}

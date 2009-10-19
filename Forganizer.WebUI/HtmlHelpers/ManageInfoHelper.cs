using System.Text;
using System.Web.Mvc;

namespace Forganizer.WebUI.HtmlHelpers
{
    public static class ManageInfoHelper
    {
        public static string ManageInfo(this HtmlHelper html)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div>tags delimited by");
            foreach (var delimiter in Forganizer.DomainModel.Constants.Delimiters) sb.Append(" [" + delimiter + "]");
            sb.AppendLine("</div>");
            sb.AppendLine("            <div>excluded extensions override included ones</div>");
            sb.AppendLine("            <div>if 'recursive' is checked, forganizer will scan down into all folders contained within " + 
                "the one specified, otherwise it will simply search that exact folder</div>");
            return sb.ToString();
        }
    }
}

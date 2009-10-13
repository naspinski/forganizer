using System.Text;
using System.Web.Mvc;

namespace Forganizer.WebUI.HtmlHelpers
{
    public static class DelimiterInfoHelper
    {
        public static string DelimiterInfo(this HtmlHelper html)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("            <div>tags delimited by");
            foreach (var delimiter in Forganizer.DomainModel.Constants.Delimiters) sb.Append(" [" + delimiter + "]");
            sb.Append("</div>");
            return sb.ToString();
        }
    }
}

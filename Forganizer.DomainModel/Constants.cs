using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forganizer.DomainModel
{
    public static class Constants
    {
        public static string UrlDelimiter { get { return "|"; } }
        public static string[] Delimiters { get { return new string[] { "|", ";", " ", ","}; } }
    }
}

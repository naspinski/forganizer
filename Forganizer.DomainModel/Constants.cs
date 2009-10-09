using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Forganizer.DomainModel
{
    public static class Constants
    {
        public static string UrlDelimiter { get { return "|"; } }
        public static string[] Delimiters { get { return new string[] { "|", ";", " ", ","}; } }
        public static decimal TagMaxSize { get { return 2; } }
        public static decimal TagMinSize { get { return 1; } }
    }
}

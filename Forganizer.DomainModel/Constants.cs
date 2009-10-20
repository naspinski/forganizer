using Forganizer.DomainModel.Entities;

namespace Forganizer.DomainModel
{
    public static class Constants
    {
        public static string UrlDelimiter { get { return ","; } }
        public static string[] Delimiters { get { return new string[] { ";", " ", ","}; } }
        public static decimal TagMaxSize { get { return 2; } }
        public static decimal TagMinSize { get { return 1; } }
        public static SearchType DefaultSearchType { get { return SearchType.And; } }
    }
}

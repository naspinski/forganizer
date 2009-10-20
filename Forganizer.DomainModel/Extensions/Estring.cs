using System;
using System.Collections.Generic;
using System.Linq;

namespace Forganizer.DomainModel.Extensions
{
    public static class Estring
    {
        public static List<string> SplitTags(this string tagString)
        {
            if (tagString == null) return new List<string>();
            if (tagString.Length == 0) return new List<string>();
            else return tagString.Split(Constants.Delimiters, StringSplitOptions.RemoveEmptyEntries).OrderBy(x => x).ToList();
        }

        public static string GetExtension(this string filePath)
        {
            int dotPosition = filePath.LastIndexOf('.');
            if (dotPosition > 0) return filePath.Substring(dotPosition, filePath.Length - dotPosition).Trim();
            else return string.Empty;
        }

        public static string AddToSearch(this object previous, string add)
        {
            return AlterSearch(previous.ToString(), add, SearchAddRemove.Add);
        }

        public static string RemoveFromSearch(this object previous, string remove)
        {
            return AlterSearch(previous.ToString(), remove, SearchAddRemove.Remove);
        }

        private enum SearchAddRemove { Add, Remove };
        private static string AlterSearch(string previous, string newOnes, SearchAddRemove searchAddRemove)
        {
            List<string> tags = previous.SplitTags();
            IEnumerable<string> newTags = newOnes.SplitTags();

            if(searchAddRemove == SearchAddRemove.Add) foreach (string tag in newTags.Except(tags)) tags.Add(tag);
            else foreach (string tag in newTags.Intersect(tags)) tags.Remove(tag);

            string[] tagArray = tags.ToArray();
            string searchString = string.Empty;
            for (int i = 0; i < tagArray.Length; i++) searchString += (i == 0 ? "" : Constants.UrlDelimiter) + tagArray[i];
            return searchString;
        }
    }
}

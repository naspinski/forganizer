using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forganizer.DomainModel.Entities;
using Forganizer.DomainModel.Abstract;

namespace Forganizer.DomainModel.Extensions
{
    public static class EIEnumerableFileObject
    {
        public static int TagCount(this IEnumerable<FileObject> fobs, string tag)
        {
            return fobs.Tags().Where(x => x.Key == tag).FirstOrDefault().Value;
        }

        public static IEnumerable<FileObject> WithTags(this IEnumerable<FileObject> fobs, string tags, SearchType searchType)
        {
            if(tags.Trim().Length == 0) return fobs;
            IEnumerable<FileObject> fobsWithTags = fobs;
            foreach (string tag in tags.Split(Constants.Delimiters, StringSplitOptions.RemoveEmptyEntries))
                fobsWithTags = fobsWithTags.WithTag(tag);
            return fobsWithTags;
        }

        private static IEnumerable<FileObject> WithTag(this IEnumerable<FileObject> fobs, string tag)
        {
            return fobs.Where(x => x.Tags.Contains(tag));
        }

        public static IEnumerable<FileObject> WithExtension(this IEnumerable<FileObject> fobs, string extensions)
        {

        }

        public static Dictionary<string, int> Tags(this IEnumerable<FileObject> fobs)
        {
            Dictionary<string, int> tags = new Dictionary<string,int>();
            IEnumerable<string> allTags = AllTags(fobs.Select(x => x.Tags));
            foreach (string tag in allTags.Distinct()) tags.Add(tag, allTags.Where(x => x == tag).Count());
            return tags;
        }

        private static IEnumerable<string> AllTags(IEnumerable<IEnumerable<string>> allTagLists)
        {
            IEnumerable<string> allTags = new List<string>();
            foreach(IEnumerable<string> tagList in allTagLists)
                allTags = allTags.Concat(tagList);
            return allTags;
        }        
    }
}

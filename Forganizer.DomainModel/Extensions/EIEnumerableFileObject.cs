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

        public static IEnumerable<FileObject> WithTags(this IEnumerable<FileObject> fobs, string tagString, SearchType searchType)
        {
            if(tagString.Trim().Length == 0) return fobs;
            IEnumerable<FileObject> fobsWithTags = fobs;
            string[] tags = tagString.Split(Constants.Delimiters, StringSplitOptions.RemoveEmptyEntries);
            for(int i = 0; i < tags.Length; i++)
            {
                if(i == 0 && searchType == SearchType.Or) fobsWithTags = fobs.WithTag(tags[i]);
                else fobsWithTags = (searchType == SearchType.And ? fobsWithTags.WithTag(tags[i]) : fobsWithTags.Union(fobs.WithTag(tags[i])));
            }
            return fobsWithTags;
        }

        private static IEnumerable<FileObject> WithTag(this IEnumerable<FileObject> fobs, string tag)
        {
            return fobs.Where(x => x.Tags().Contains(tag));
        }

        public static IEnumerable<FileObject> WithExtensions(this IEnumerable<FileObject> fobs, string extensionString)
        {
            if (extensionString.Trim().Length == 0) return fobs;
            string[] extensions = extensionString.Split(Constants.Delimiters, StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<FileObject> fobsWithExtensions = fobs.WithExtension(extensions[0]);
            for (int i = 1; i < extensions.Length; i++)
                fobsWithExtensions = fobsWithExtensions.Union(fobs.WithExtension(extensions[i]));
            return fobsWithExtensions;
        }

        private static IEnumerable<FileObject> WithExtension(this IEnumerable<FileObject> fobs, string extension)
        {
            return fobs.Where(x => x.FileInfo().Extension == extension);
        }

        public static Dictionary<string, int> Tags(this IEnumerable<FileObject> fobs)
        {
            Dictionary<string, int> tags = new Dictionary<string,int>();
            IEnumerable<string> allTags = AllTags(fobs.Select(x => x.Tags()));
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

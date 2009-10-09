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
            try { return fobs.Tags().Where(x => x.Name == tag).FirstOrDefault().Count; }
            catch (NullReferenceException) { return 0; }
            catch (Exception ex) { throw ex; }
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
            return fobs.Where(x => x.Tags.Contains(tag));
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
            return fobs.Where(x => x.FileInfo.Extension == extension);
        }

        public static IEnumerable<Tag> Extensions(this IEnumerable<FileObject> fobs)
        {
            List<Tag> extensions = new List<Tag>();
            foreach (string ext in fobs.Select(x => x.FileInfo.Extension).Distinct())
                extensions.Add(new Tag { Name = ext, Count = fobs.Where(x => x.FileInfo.Extension == ext).Count() });
            extensions = SetSizes(extensions);
            return extensions;
        }

        public static IEnumerable<Tag> Tags(this IEnumerable<FileObject> fobs)
        {
            List<Tag> tags = new List<Tag>();
            IEnumerable<string> allTags = AllTags(fobs.Select(x => x.Tags));
            foreach (string tag in allTags.Distinct())
                tags.Add(new Tag { Name = tag, Count = allTags.Where(x => x == tag).Count() });// Add(tag, allTags.Where(x => x == tag).Count());
            tags = SetSizes(tags);
            return tags;
        }

        private static List<Tag> SetSizes(List<Tag> tags)
        {
            int max = tags.Select(x => x.Count).Max();
            foreach (Tag tag in tags)
            {
                tag.Size = (tag.Count / (decimal)max) * (decimal)Constants.TagMaxSize;
                if (tag.Size < Constants.TagMinSize) tag.Size = Constants.TagMinSize;
            }
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

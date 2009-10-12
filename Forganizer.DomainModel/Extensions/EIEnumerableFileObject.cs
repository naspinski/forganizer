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

        public static IQueryable<FileObject> WithTags(this IQueryable<FileObject> fobs, string tagString, SearchType searchType)
        {
            if(tagString.Trim().Length == 0) return fobs;
            IQueryable<FileObject> fobsWithTags = fobs;
            string[] tags = tagString.SplitTags().ToArray();
            for(int i = 0; i < tags.Length; i++)
            {
                if(i == 0 && searchType == SearchType.Or) fobsWithTags = fobs.WithTag(tags[i]);
                else fobsWithTags = (searchType == SearchType.And ? fobsWithTags.WithTag(tags[i]) : fobsWithTags.Union(fobs.WithTag(tags[i])));
            }
            return fobsWithTags;
        }

        private static IQueryable<FileObject> WithTag(this IQueryable<FileObject> fobs, string tag)
        {
            return fobs.Where(x => x.TagString.Contains(tag));
        }

        public static IQueryable<FileObject> WithExtensions(this IQueryable<FileObject> fobs, string extensionString)
        {
            if (extensionString.Trim().Length == 0) return fobs;
            string[] extensions = extensionString.SplitTags().ToArray();
            IQueryable<FileObject> fobsWithExtensions = fobs.WithExtension(extensions[0]);
            for (int i = 1; i < extensions.Length; i++)
                fobsWithExtensions = fobsWithExtensions.Union(fobs.WithExtension(extensions[i]));
            return fobsWithExtensions;
        }

        private static IQueryable<FileObject> WithExtension(this IQueryable<FileObject> fobs, string extension)
        {
            return fobs.Where(x => x.FilePath.EndsWith(extension));
        }

        public static IQueryable<Tag> Extensions(this IQueryable<FileObject> fobs)
        {
            List<Tag> extensions = new List<Tag>();
            IEnumerable<string> distinctExts = fobs.Select(x => x.FilePath.GetExtension());
            foreach (string ext in distinctExts.Distinct())
                extensions.Add(new Tag { Name = ext, Count = fobs.Where(x => x.FilePath.EndsWith(ext)).Count() });
            extensions = SetSizes(extensions);
            return extensions.AsQueryable();
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
            int max = 0;
            try { max = tags.Select(x => x.Count).Max(); }
            catch (InvalidOperationException) { } //none present
            catch (Exception ex) { throw ex; }
            foreach (Tag tag in tags)
            {
                tag.Size = (tag.Count / (decimal)max) * (decimal)Constants.TagMaxSize;
                if (tag.Size < Constants.TagMinSize) tag.Size = Constants.TagMinSize;
            }
            return tags.OrderBy(x => x.Name).ToList();
        }

        private static IEnumerable<string> AllTags(IEnumerable<IEnumerable<string>> allTagLists)
        {
            IEnumerable<string> allTags = new List<string>();
            foreach(IEnumerable<string> tagList in allTagLists)
                allTags = allTags.Concat(tagList);
            return allTags;
        }

        public static IEnumerable<Tag> Categories(this IQueryable<FileObject> fileObjects, IEnumerable<Category> categories)
        {
            if (fileObjects == null || fileObjects.Count() == 0) 
                return categories.Select(x => new Tag() { Name = x.Name, QueryString = x.ExtensionString.AddToSearch(null), Count = 1, Size = Constants.TagMinSize });
            List<Tag> categoriesPresent = new List<Tag>();
            int count;
            foreach (Category category in categories)
            {
                count = fileObjects.Extensions().Select(x => x.Name).Intersect(category.Extensions).Count();
                if (count > 0)
                    categoriesPresent.Add(new Tag() { Name = category.Name, QueryString = category.ExtensionString.AddToSearch(null), Count = count });
            }
            categoriesPresent = SetSizes(categoriesPresent);
            return categoriesPresent;
        }
    }
}

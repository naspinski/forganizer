using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Forganizer.DomainModel.Abstract;
using Forganizer.DomainModel.Extensions;

namespace Forganizer.DomainModel.Entities
{
    public static class EntityUtilities
    {
        public class Categories
        {
            public static string EnsureValid(IDataErrorInfo validatable, ICategoryRepository categoryRepository, params string[] properties)
            {
                Category category = (Category)validatable;
                EntityUtilities.EnsureValid(validatable, properties);
                if (categoryRepository.Categories.Any(x => x.Id != category.Id && x.Name == category.Name))
                    return "name is a duplicate";
                else return (string)null;
            }
        }

        public static void EnsureValid(IDataErrorInfo validatable, params string[] properties)
        {
            if (properties.Any(x => validatable[x] != null))
                throw new InvalidOperationException(validatable.GetType().ToString() + " is invalid");
        }

        public static string AddTags(string fullString, string tags)
        {
            IEnumerable<string> Tags = fullString.SplitTags();
            var newTags = tags.Split(Constants.Delimiters, StringSplitOptions.RemoveEmptyEntries);
            if (newTags.Count() == 0) throw new FormatException("tags entry is empty");
            else foreach (string tag in newTags.Except(Tags)) fullString = AddTag(fullString, Tags, tag.Trim());
            return fullString;
        }

        private static string AddTag(string TagString, IEnumerable<string> Tags, string tag)
        {
            if (string.IsNullOrEmpty(tag)) throw new FormatException("tag entered is empty");
            if (Tags.Contains(tag)) throw new FormatException("duplicate entered [" + tag + "]");
            if (tag.SplitTags().Count() > 1) throw new FormatException("delimiters not allowed in a tag");
            else return TagString += (TagString.Length > 0 ? Constants.UrlDelimiter : "") + tag;
        }

        public static string DeleteTag(string TagString, string tag)
        {
            return TagString.RemoveFromSearch(tag).Replace(Constants.UrlDelimiter, " ");
        }
    }
}

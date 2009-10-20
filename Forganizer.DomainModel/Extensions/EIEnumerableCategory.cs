using System.Collections.Generic;
using System.Linq;
using Forganizer.DomainModel.Entities;

namespace Forganizer.DomainModel.Extensions
{
    public static class EIEnumerableCategory
    {
        public static IEnumerable<Tag> GetCategoryTags(this IEnumerable<Category> categories, IEnumerable<Tag> extensions)
        {
            IEnumerable<KeyValuePair<string, int>> activeExtensions = extensions.Where(x => x.Active).Select(x => new KeyValuePair<string, int>(x.Name, x.Count));
            IEnumerable<KeyValuePair<string, int>> inactiveExtensions = extensions.Where(x => !x.Active).Select(x => new KeyValuePair<string, int>(x.Name, x.Count));
            Dictionary<bool, IEnumerable<KeyValuePair<string, int>>> iterator = new Dictionary<bool,IEnumerable<KeyValuePair<string,int>>>();
            iterator.Add(true, activeExtensions);
            iterator.Add(false, inactiveExtensions);
            List<Tag> categoryCollection = new List<Tag>();
            int count = 0;
            foreach (Category category in categories)
            {
                foreach(var kvp in iterator)
                {
                    count =kvp.Value.Where(x => category.Extensions.Contains(x.Key)).Select(x => x.Value).Sum();
                    if (count > 0)
                    {
                        categoryCollection.Add(new Tag()
                        {
                            Name = category.Name,
                            Size = 1,
                            QueryString = category.ExtensionString,
                            Active = kvp.Key,
                            Count = count,
                        });
                    }
                }
            }
            return categoryCollection.SetSizes();
        }
    }
}

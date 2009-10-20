using System;
using System.Collections.Generic;
using System.Linq;
using Forganizer.DomainModel.Entities;

namespace Forganizer.DomainModel.Extensions
{
    public static class EListTag
    {
        public static List<Tag> SetSizes(this List<Tag> tags)
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
    }
}

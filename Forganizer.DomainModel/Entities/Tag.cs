using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forganizer.DomainModel.Extensions;

namespace Forganizer.DomainModel.Entities
{
    public enum TagType { Tag, Extension, Category }
    public class Tag
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Size { get; set; }
        public string QueryString { get; set; }
        public bool Active { get; set; }
        public IEnumerable<string> QueryStringTags { get { return QueryString.SplitTags(); } }
    }
}

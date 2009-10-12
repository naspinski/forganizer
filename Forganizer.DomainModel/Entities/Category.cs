using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using Forganizer.DomainModel.Extensions;

namespace Forganizer.DomainModel.Entities
{
    [Table(Name = "Categories")]
    public class Category
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }
        [Column] public string Name { get; set; }
        [Column] public string ExtensionString { get; set; }
        public IEnumerable<string> Extensions { get { return ExtensionString.SplitTags(); } }
    }
}

using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using Forganizer.DomainModel.Extensions;

namespace Forganizer.DomainModel.Entities
{
    [Table(Name = "Categories")]
    public class Category : IDataErrorInfo
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }
        [Column] public string Name { get; set; }
        [Column] public string ExtensionString { get; set; }
        public IEnumerable<string> Extensions { get { return ExtensionString.SplitTags(); } }

        public void AddExtensions(string extensions)
        {
            ExtensionString = EntityUtilities.AddTags(ExtensionString, extensions);
        }

        public void DeleteExtensions(string tag)
        {
            ExtensionString = EntityUtilities.DeleteTag(ExtensionString, tag);
        }

        public string this[string propName]
        {
            get
            {
                if (propName == "Name" && string.IsNullOrEmpty(Name)) return "category name can't be blank";
                return null;
            }
        }
        public string Error { get { return null; } }
    }
}

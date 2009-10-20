using System.ComponentModel;
using Forganizer.DomainModel.Entities;

namespace Forganizer.WebUI.Models
{
    public class FileDeleteModel : Folder, IDataErrorInfo
    {
        public string WithTags { get; set; }
        public string ExcludeTags { get; set; }

        public string this[string propName] { get { return null; } }
    }
}

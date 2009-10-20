using System.ComponentModel;
using System.Text.RegularExpressions;
using Forganizer.DomainModel.Entities;

namespace Forganizer.WebUI.Models
{
    public enum TagEditType { Edit, Delete, Add };
    public class TagEditModel : Folder, IDataErrorInfo
    {
        public string Replace { get; set; }
        public string With { get; set; }
        public TagEditType EditType { get; set; }

        public string this[string propName]
        {
            get
            {
                if (propName == "Path" && !string.IsNullOrEmpty(Path) && !Regex.IsMatch(Path, @"^\\\\.+\\.+"))
                        return "invalid network path (ex: '\\\\server\\share')";
                if (propName == "Replace" && string.IsNullOrEmpty(Replace)) 
                    return "need to specify what to replace";
                if (propName == "With" && string.IsNullOrEmpty(With)) 
                    return "need to specify what to replace with";
                return null;
            }
        }
    }
}

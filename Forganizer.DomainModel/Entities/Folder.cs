using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forganizer.DomainModel.Extensions;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Forganizer.DomainModel.Entities
{
    public class Folder : IDataErrorInfo
    {
        public string Path { get; set; }
        public string Exclude { get; set; }
        public string Include { get; set; }
        public bool Recursive { get; set; }

        public IEnumerable<string> ExcludeExtensions { get { return Exclude.SplitTags(); } }
        public IEnumerable<string> IncludeExtensions { get { return Include.SplitTags(); } }

        public string this[string propName]
        {
            get
            {
                if (propName == "Path")
                {
                    if(string.IsNullOrEmpty(Path)) return "path can't be blank";
                    if (!Regex.IsMatch(Path, @"^\\\\[^\/\\:*?""<>|]+\\[^\/\\:*?""<>|]+)(\\[^\/\\:*?""<>|]+)+(\.[^\/\\:*?""<>|]+)$"))
                        return "invalid network path (starts with '\\\\')";
                }
                return null;
            }
        }
        public string Error { get { return null; } } 
    }
}

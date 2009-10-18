using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Forganizer.DomainModel.Extensions;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Forganizer.DomainModel.Abstract;

namespace Forganizer.DomainModel.Entities
{
    public class Folder : IDataErrorInfo
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Exclude { get; set; }
        public string Include { get; set; }
        public bool Recursive { get; set; }

        public IEnumerable<string> ExcludeExtensions { get { return Exclude.SplitTags(); } }
        public IEnumerable<string> IncludeExtensions { get { return Include.SplitTags(); } }


        public IEnumerable<string> AddFilesTo(IFileObjectRepository fileObjects)
        {
            List<string> report = new List<string>();

            foreach(char c in System.IO.Path.GetInvalidPathChars())
                Include = Include.Replace(c.ToString(), string.Empty);

            IEnumerable<string> include = string.IsNullOrEmpty(Include) ? new List<string> { "*" } : Include.SplitTags().Select(x => "*" + x);
            IEnumerable<string> exclude = Exclude.SplitTags();

            foreach (string extension in include)
            {
                foreach (string filePath in Directory.GetFiles(Path, extension, (Recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)))
                {
                    if (!exclude.Any(x => filePath.EndsWith(x)))
                    {
                        FileObject fileObject = fileObjects.GetFileObject(filePath);

                        if (!fileObject.Active)
                        {
                            bool isOld = fileObject.FilePath != null;
                            fileObject.FilePath = filePath;
                            fileObject.Active = true;

                            fileObjects.SaveFileObject(fileObject);
                            report.Add(filePath + " " + (isOld ? "restored" : "added"));
                        }
                    }
                }
                fileObjects.SubmitChanges();
            }
            if (report.Count == 0) report.Add("nothing new added to forganizer");
            return report;
        }

        public string this[string propName]
        {
            get
            {
                if (propName == "Path")
                {
                    if (string.IsNullOrEmpty(Path)) return "path can't be blank";
                    if (!Regex.IsMatch(Path, @"^\\\\.+\\.+")) return "invalid network path (ex: '\\\\server\\share')";
                }
                return null;
            }
        }
        public string Error { get { return null; } }
    }
}

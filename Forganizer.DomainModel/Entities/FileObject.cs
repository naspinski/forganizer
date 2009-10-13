using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using Forganizer.DomainModel.Extensions;

namespace Forganizer.DomainModel.Entities
{
    public enum SearchType { And, Or };

    [Table(Name = "FileObjects")]
    public class FileObject : IDataErrorInfo
    {
        public FileObject() { TagString = string.Empty; }

        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)] public int Id { get; set; }
        [Column] public string FilePath { get; set; }
        [Column] public string Name { get; private set; }
        [Column] public string TagString { get; set; }
        [Column] public DateTime Modified { get; set; }
        [Column] public bool Active { get; set; }

        public FileInfo FileInfo { get { return new FileInfo(FilePath); } }
        public IEnumerable<string> Tags { get { return TagString.SplitTags(); } }

        public void AddTags(string tags)
        {
            var newTags = tags.Split(Constants.Delimiters, StringSplitOptions.RemoveEmptyEntries);
            if (newTags.Count() == 0)  throw new FormatException("tags entry is empty");
            else  foreach (string tag in newTags) AddTag(tag.Trim());
        }

        public void AddTag(string tag)
        {
            if (string.IsNullOrEmpty(tag)) throw new FormatException("tag entered is empty");
            if (Tags.Contains(tag)) throw new FormatException("duplicate entered [" + tag + "]");
            if(tag.SplitTags().Count() > 1) throw new FormatException("delimiters not allowed in a tag");
            else TagString += (TagString.Length > 0 ? Constants.UrlDelimiter : "") + tag;
        }

        public string this[string propName]
        {
            get
            {
                if (propName == "FilePath" && string.IsNullOrEmpty(FilePath)) return "FilePath can't be empty";
                if (propName == "Name" && string.IsNullOrEmpty(Name)) return "Name can't be blank (code error)";
                return null;
            }
        }
        public string Error { get { return null; } } 
    }
}
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
        [Column] public bool Active { get; set; }

        public FileInfo FileInfo { get { return new FileInfo(FilePath); } }
        public IEnumerable<string> Tags { get { return TagString.SplitTags(); } }

        public void AddTags(string tags)
        {
            TagString = EntityUtilities.AddTags(TagString, tags);
        }

        public void DeleteTags(string tag)
        {
            TagString = EntityUtilities.DeleteTag(TagString, tag);
        }

        public void UpdateName()
        {
            Name = FileInfo.Name;
        }

        public void ReplaceTags(string replace, string with)
        {
            if(!string.IsNullOrEmpty(replace.Trim())) DeleteTags(replace);
            if(!string.IsNullOrEmpty(with.Trim())) AddTags(with);
        }

        public string this[string propName]
        {
            get
            {
                if (propName == "FilePath" && string.IsNullOrEmpty(FilePath)) return "FilePath can't be empty";
                return null;
            }
        }
        public string Error { get { return null; } }
    }
}
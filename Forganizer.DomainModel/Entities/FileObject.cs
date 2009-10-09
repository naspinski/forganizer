﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.IO;
using Forganizer.DomainModel.Abstract;
using Forganizer.DomainModel.Concrete;

namespace Forganizer.DomainModel.Entities
{
    public enum SearchType { And, Or };

    [Table(Name = "FileObjects")]
    public class FileObject
    {
        public FileObject() { TagString = string.Empty; }

        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)] public int Id { get; set; }
        [Column] public string FilePath { get; set; }
        [Column(Name="tags")] public string TagString { get; set; }
        [Column] public DateTime Modified { get; set; }
        [Column] public bool Active { get; set; }

        public FileInfo FileInfo { get { return new FileInfo(FilePath); } }
        public IEnumerable<string> Tags
        {
            get
            {
                if (TagString.Length == 0) return new List<string>();
                else return TagString.Split(new string[] { Constants.UrlDelimiter }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
        }

        public void AddTags(string tags)
        {
            if (string.IsNullOrEmpty(tags.Replace(Constants.UrlDelimiter, string.Empty).Trim())) 
                throw new FormatException("tags entry is empty");
            else 
                foreach (string tag in tags.Split(Constants.Delimiters, StringSplitOptions.RemoveEmptyEntries)) AddTag(tag.Trim());
        }

        public void AddTag(string tag)
        {
            if (string.IsNullOrEmpty(tag)) throw new FormatException("tag entered is empty");
            else if (Tags.Contains(tag)) throw new FormatException("duplicate entered [" + tag + "]");
            else if (tag.Contains(Constants.UrlDelimiter)) throw new FormatException("'" + Constants.UrlDelimiter + "' is not allowed in a tag");
            else TagString += (TagString.Length > 0 ? Constants.UrlDelimiter : "") + tag;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forganizer.DomainModel.Entities;

namespace Forganizer.WebUI.Models
{
    public class FileAndTagCollection
    {
        public IEnumerable<FileObject> PageOfFileObjects { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<Tag> Extensions { get; set; }
        public IEnumerable<Tag> Categories { get; set; }
    }
}
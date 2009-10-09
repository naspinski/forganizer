using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Forganizer.DomainModel.Entities;
using Forganizer.DomainModel.Extensions;
using Forganizer.DomainModel;

namespace Forganizer.Tests.DomainModel
{
    [TestFixture]
    class FileObjectTests
    {
        [Test]
        public void FileInfo_returns_the_proper_things()
        {
            FileObject file = new FileObject { FilePath = @"C:\some_folder\with a space\file.gif" };

            Assert.AreEqual(".gif", file.FileInfo.Extension);
            Assert.AreEqual(@"C:\some_folder\with a space", file.FileInfo.DirectoryName);
        }

        [Test]
        public void Tags_functions_are_working()
        {
            FileObject file = new FileObject();
            file.AddTag("abc");
            file.AddTag("def");

            Assert.IsTrue(file.Tags.Contains("abc"));
            Assert.IsTrue(file.Tags.Contains("def"));
        }

        [Test]
        public void FileName_returns_correct_string()
        {
            FileObject file = new FileObject { FilePath = @"C:\some_folder\with a space\file.gif" };

            Assert.AreEqual("file.gif", file.FileInfo.Name);
        }
    }
}

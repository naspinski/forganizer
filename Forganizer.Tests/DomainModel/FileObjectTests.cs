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
        FileObject file;
        [SetUp]
        public void Setup()
        {
           file = new FileObject { FilePath = @"C:\some_folder\with a space\file.gif" };
        }

        [Test]
        public void Tags_functions_are_working()
        {
            FileObject file = new FileObject();
            file.AddTags("abc");
            file.AddTags("def");

            Assert.IsTrue(file.Tags.Contains("abc"));
            Assert.IsTrue(file.Tags.Contains("def"));
        }

        [Test]
        public void FileInfo_returns_correct_string()
        {
            Assert.AreEqual("file.gif", file.FileInfo.Name);
        }

        [Test]
        public void UpdateName_updates_name_correctly()
        {
            file.UpdateName();
            Assert.AreEqual("file.gif", file.Name);
        }

        [Test]
        public void Add_and_remove_tags_works()
        {
            Assert.AreEqual(0, file.Tags.Count());
            file.AddTags("a b c");
            Assert.AreEqual(3, file.Tags.Count());
            file.AddTags("a");
            Assert.AreEqual(3, file.Tags.Count());
            file.DeleteTags("DOESNT_EXIST");
            Assert.AreEqual(3, file.Tags.Count());
            file.DeleteTags("a;b");
            Assert.AreEqual(1, file.Tags.Count());
        }

        [Test]
        public void ReplaceTags()
        {
            file.AddTags("a b c");
            file.ReplaceTags("a", "x y");
            Assert.AreEqual(4, file.Tags.Count());
        }
    }
}

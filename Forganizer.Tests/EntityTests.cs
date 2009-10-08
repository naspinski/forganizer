using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Forganizer.DomainModel.Entities;
using Forganizer.DomainModel.Extensions;

namespace Forganizer.Tests
{
    [TestFixture]
    class EntityTests
    {
        [Test]
        public void FileInfo_returns_the_proper_things()
        {
            FileObject file = new FileObject { FilePath = "C:\\some_folder\\with a space\\file.gif" };

            Assert.AreEqual(".gif", file.FileInfo.Extension);
            Assert.AreEqual("C:\\some_folder\\with a space", file.FileInfo.DirectoryName);
        }

        [Test]
        public void Tags_functions_are_working()
        {
            FileObject file = new FileObject();
            file.AddTag("abc");
            file.AddTag("def");

            //Assert.IsTrue(file.Tags.Contains("abc"));
            //Assert.IsTrue(file.Tags.Contains("def"));
        }

        [Test]
        public void Pulling_all_tags_from_an_ienumerable_yields_proper_results()
        {
            IEnumerable<FileObject> fobs = Utilities.Mocking.MockFileObjectRepository().FileObjects;

            Assert.AreEqual(4, fobs.Tags().Count);
            Assert.AreEqual(2, fobs.TagCount("funny"));
            Assert.AreNotEqual(2, fobs.TagCount("non-existant tag"));
            Assert.AreEqual(0, fobs.TagCount("non-existant tag"));
        }

        [Test]
        public void Category_string_works()
        {
            Category cat = new Category { Name = "music" };
            cat.Extensions.Add(".mp3");
            cat.Extensions.Add(".wma");

            Assert.AreEqual(".mp3|.wma", cat.ExtensionString);
        }
    }
}

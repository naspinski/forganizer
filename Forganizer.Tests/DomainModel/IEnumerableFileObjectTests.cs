using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Forganizer.DomainModel.Entities;
using Forganizer.DomainModel.Extensions;
using Forganizer.DomainModel;
using Forganizer.DomainModel.Abstract;
using Moq;

namespace Forganizer.Tests.DomainModel
{
    [TestFixture]
    class IEnumerableFileObjectTests
    {
        IQueryable<FileObject> fobs = Utilities.Mocking.MockFileObjectRepository().Object.FileObjects;

        [Test]
        public void Pulling_all_tags_from_an_ienumerable_yields_proper_results()
        {
            Assert.AreEqual(4, fobs.Tags(true).Count());
            Assert.AreEqual(2, fobs.TagCount("funny"));
            Assert.AreNotEqual(2, fobs.TagCount("non-existant tag"));
            Assert.AreEqual(0, fobs.TagCount("non-existant tag"));
        }

        [Test]
        public void FileObjects_with_Tags()
        {
            Assert.AreEqual(2, fobs.WithTags("funny", SearchType.Or).Count());
            Assert.AreEqual(1, fobs.WithTags("long", SearchType.Or).Count());
            Assert.AreEqual(0, fobs.WithTags("non-existant-tag", SearchType.Or).Count());
            Assert.AreEqual(3, fobs.WithTags("", SearchType.Or).Count());
            Assert.AreEqual(3, fobs.WithTags("", SearchType.And).Count());
            Assert.AreEqual(3, fobs.WithTags("funny" + Constants.UrlDelimiter + "big", SearchType.Or).Count());
            Assert.AreEqual(1, fobs.WithTags("funny" + Constants.UrlDelimiter + "big", SearchType.And).Count());
        }

        [Test]
        public void FileObjects_with_extensions()
        {
            Assert.AreEqual(0, fobs.WithExtensions(".fake").Count());
            Assert.AreEqual(1, fobs.WithExtensions(".txt" + Constants.UrlDelimiter + ".fake").Count());
            Assert.AreEqual(3, fobs.WithExtensions(".jpg" + Constants.UrlDelimiter + ".txt").Count());
            Assert.AreEqual(3, fobs.WithExtensions("").Count());
        }

        [Test]
        public void Categories()
        {
            List<FileObject> fileObjects = new List<FileObject>() {
                new FileObject() { Id=1, FilePath=@"C:\some_folder\another one\image.jpg", Active=true, TagString="funny big" },
                new FileObject() { Id=2, FilePath=@"C:\some_folder\second\another_image.jpg", Active=true, TagString="serious big" },
                new FileObject() { Id=3, FilePath=@"C:\some_folder\second\textfile.txt", Active=true, TagString="funny long" },
                new FileObject() { Id=3, FilePath=@"C:\some_folder\second\ninjas.txt", Active=false, TagString="turds" }
            };

            List<Category> categories = new List<Category>() {
                new Category() { Id=1, Name="video", ExtensionString=".mpg .avi"},
                new Category() { Id=1, Name="image", ExtensionString=".jpg"},
                new Category() { Id=2, Name="text", ExtensionString=".doc txt"},
            };

            IEnumerable<Tag> tags = fileObjects.AsQueryable().Categories(categories, true);

            Assert.AreEqual(2, tags.Count());
            Assert.IsTrue(tags.Any(x => x.Name == "image"));
            Assert.IsTrue(tags.Any(x => x.Name == "text"));
            Assert.IsFalse(tags.Any(x => x.Name == "video"));
            Assert.AreEqual(2, tags.First(x => x.Name == "image").Count);
            Assert.AreEqual(2, tags.First(x => x.Name == "text").Count);
        }
    }
}

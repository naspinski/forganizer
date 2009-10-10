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
    class IEnumerableFileObjectTests
    {
        IEnumerable<FileObject> fobs = Utilities.Mocking.MockFileObjectRepository().FileObjects;

        [Test]
        public void Pulling_all_tags_from_an_ienumerable_yields_proper_results()
        {
            Assert.AreEqual(4, fobs.Tags().Count());
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
    }
}

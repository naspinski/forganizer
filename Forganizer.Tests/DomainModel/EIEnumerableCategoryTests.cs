using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Forganizer.DomainModel.Entities;
using Forganizer.DomainModel.Extensions;

namespace Forganizer.Tests.DomainModel
{
    [TestFixture]
    class EIEnumerableCategoryTests
    {
        [Test]
        public void GetCategoryTags()
        {
            List<Category> categories = new List<Category>() {
                new Category() { Id=1, Name="video", ExtensionString=".mpg .avi"},
                new Category() { Id=2, Name="image", ExtensionString=".jpg"},
                new Category() { Id=3, Name="text", ExtensionString=".doc .txt"},
            };
            List<Tag> tags = new List<Tag>() { 
                new Tag() { Name=".txt", Count=1, QueryString=".txt", Active=true},
                new Tag() { Name=".avi", Count=1, QueryString=".avi", Active=true},
                new Tag() { Name=".doc", Count=1, QueryString=".doc", Active=true},
                new Tag() { Name=".jpg", Count=1, QueryString=".jpg", Active=false},
            };

            tags = categories.GetCategoryTags(tags).ToList();

            Assert.AreEqual(2, tags.First(x => x.Name == "text").Count);
            Assert.AreEqual(1, tags.First(x => x.Name == "image").Count);
            Assert.AreEqual(1, tags.First(x => x.Name == "video").Count);
            Assert.AreEqual(1, tags.Where(x => !x.Active).Count());
            Assert.AreEqual(2, tags.Where(x => x.Active).Count());
        }
    }
}

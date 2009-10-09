using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Forganizer.DomainModel.Entities;
using Forganizer.DomainModel;

namespace Forganizer.Tests.DomainModel
{
    [TestFixture]
    class CategoryTests
    {
        [Test]
        public void Category_string_works()
        {
            Category cat = new Category { Name = "music" };
            cat.Extensions.Add(".mp3");
            cat.Extensions.Add(".wma");

            Assert.AreEqual(".mp3" + Constants.UrlDelimiter + ".wma", cat.ExtensionString);
        }
    }
}

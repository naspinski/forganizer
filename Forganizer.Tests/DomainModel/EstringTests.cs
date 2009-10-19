using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Forganizer.DomainModel.Extensions;
using Forganizer.DomainModel;

namespace Forganizer.Tests.DomainModel
{
    [TestFixture]
    class EstringTests
    {
        [Test]
        public void GetExtension_returns_extension()
        {
            Assert.AreEqual(".txt", "asbdsafds.asdasd.asdasd.asd.txt".GetExtension());
            Assert.AreEqual(string.Empty, "asbd/asdsad/asdasdasdasd".GetExtension());
        }

        [Test]
        public void AddToSearch_works()
        {
            Assert.AreEqual("abbc" + Constants.UrlDelimiter + "def", "abbc".AddToSearch(" def"));
            Assert.AreEqual("xxx", string.Empty.AddToSearch("xxx"));
        }
    }
}

using Forganizer.DomainModel;
using Forganizer.DomainModel.Extensions;
using NUnit.Framework;

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

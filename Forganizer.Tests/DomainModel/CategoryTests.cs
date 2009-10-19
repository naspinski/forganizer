using System.Linq;
using Forganizer.DomainModel.Entities;
using NUnit.Framework;

namespace Forganizer.Tests.DomainModel
{
    [TestFixture]
    class CategoryTests
    {
        Category category;
        [SetUp]
        public void Setup()
        {
            category = new Category() { ExtensionString = ".abc .xyz" };
        }

        [Test]
        public void Category_Extensions_works()
        {
            Assert.AreEqual(2, category.Extensions.Count());
            Assert.AreEqual(".xyz", category.Extensions.Last());
        }

        [Test]
        public void Add_and_delete_extensions_works()
        {
            Assert.AreEqual(2, category.Extensions.Count());
            category.AddExtensions("zzz");
            Assert.AreEqual(3, category.Extensions.Count());
            category.AddExtensions("zzz"); // adding a duplicate doesn't add another
            Assert.AreEqual(3, category.Extensions.Count());
            category.AddExtensions("a b c");
            Assert.AreEqual(6, category.Extensions.Count());
            category.DeleteExtensions("not-really-there");
            Assert.AreEqual(6, category.Extensions.Count());
            category.DeleteExtensions("a");
            Assert.AreEqual(5, category.Extensions.Count());
            category.DeleteExtensions("b c");
            Assert.AreEqual(3, category.Extensions.Count());
        }
    }
}

using System.Collections.Generic;
using Forganizer.DomainModel.Extensions;
using NUnit.Framework;

namespace Forganizer.Tests.DomainModel
{
    [TestFixture]
    class EIEnumerableStringTests
    {
        [Test]
        public void ToSpacedString()
        {
            List<string> strings = new List<string>() { "a", "b", "c" };
            Assert.AreEqual("a b c", strings.ToSpacedString());
        }
    }
}

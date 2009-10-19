using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Forganizer.DomainModel.Extensions;

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

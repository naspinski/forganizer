using System.Collections.Generic;
using System.Linq;
using Forganizer.DomainModel.Extensions;
using NUnit.Framework;

namespace Forganizer.Tests.DomainModel
{
    [TestFixture]
    class EKeyValuePairStringIntTests
    {
        [Test]
        public void TagSize_returns_the_proper_tagsize()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic.Add("a", 1);

            Assert.AreEqual(1, dic.First().TagSize(dic.Select(x => x.Value).Sum(), 0, 2));
        }
    }
}

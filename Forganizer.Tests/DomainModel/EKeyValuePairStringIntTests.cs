using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Forganizer.DomainModel.Extensions;

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

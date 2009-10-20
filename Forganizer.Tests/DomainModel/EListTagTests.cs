using System.Collections.Generic;
using System.Linq;
using Forganizer.DomainModel;
using Forganizer.DomainModel.Entities;
using Forganizer.DomainModel.Extensions;
using NUnit.Framework;

namespace Forganizer.Tests.DomainModel
{
    [TestFixture]
    class EListTagTests
    {
        [Test]
        public void SetSizes()
        {
            List<Tag> tags = new List<Tag>() {
                new Tag() { Name="middle", Count=2 },
                new Tag() { Name="most", Count=3 },
                new Tag() { Name="least", Count=1 }
            };

            tags = tags.SetSizes();

            Assert.AreEqual(Constants.TagMaxSize, tags.First(x => x.Name == "most").Size);
            Assert.AreEqual(Constants.TagMinSize, tags.First(x => x.Name == "least").Size);
        }
    }
}

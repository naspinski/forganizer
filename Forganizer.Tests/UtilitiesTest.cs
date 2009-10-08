using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Forganizer.DomainModel.Abstract;

namespace Forganizer.Tests
{
    [TestFixture]
    class UtilitiesTest
    {
        [Test]
        public void Mocking_the_repository_works()
        {
            IFileObjectRepository fileObjectRepository = Utilities.Mocking.MockFileObjectRepository();
            Assert.AreEqual(".jpg", fileObjectRepository.FileObjects.First().FileInfo.Extension);
        }
    }
}

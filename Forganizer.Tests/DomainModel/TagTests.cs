using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Forganizer.DomainModel.Entities;

namespace Forganizer.Tests.DomainModel
{
    [TestFixture]
    class TagTests
    {
        [Test]
        public void Tag_size_returns_proper_size()
        {
            IEnumerable<FileObject> fobs = Utilities.Mocking.MockFileObjectRepository().FileObjects;


        }
    }
}

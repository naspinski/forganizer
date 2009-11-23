using System.Collections.Generic;
using System.Linq;
using Forganizer.DomainModel.Abstract;
using Forganizer.DomainModel.Entities;
using Forganizer.WebUI.Controllers;
using Forganizer.WebUI.Models;
using Moq;
using NUnit.Framework;

namespace Forganizer.Tests.WebUI
{
    [TestFixture]
    class ManageControllerTests
    {
        Mock<IFileObjectRepository> mockFileObjectRepository;
        TagEditModel tagEditModel;
        List<FileObject> fobs;
        [SetUp]
        public void Setup()
        {
            fobs = new List<FileObject>() {
                new FileObject() { Id=1, FilePath=@"C:\some_folder\another one\image.jpg", Active=true, TagString="funny big" },
                new FileObject() { Id=2, FilePath=@"C:\some_folder\second\another_image.jpg", Active=true, TagString="serious big" },
                new FileObject() { Id=3, FilePath=@"C:\some_folder\second\textfile.txt", Active=true, TagString="funny long" },
                new FileObject() { Id=3, FilePath=@"C:\some_folder\second\ninjas.txt", Active=false, TagString="turds" }
            };
            mockFileObjectRepository = new Mock<IFileObjectRepository>();
            mockFileObjectRepository.Setup(x => x.FileObjects).Returns(fobs.AsQueryable().Where(x => x.Active));
            mockFileObjectRepository.Setup(x => x.AllFileObjects).Returns(fobs.AsQueryable());

            tagEditModel = new TagEditModel() { EditType = TagEditType.Edit, Replace = "a", With="b" }; 
        }

        [Test]
        public void Tags_which_tests_setEditType()
        {
            Assert.AreEqual(TagEditType.Edit, tagEditModel.EditType);
            ManageController controller = new ManageController(fobs as IFileObjectRepository);
            
            controller.Tags("Add");
            TagEditModel result = controller.ViewData.Model as TagEditModel;

            Assert.AreEqual(TagEditType.Add, result.EditType);
        }
    }
}

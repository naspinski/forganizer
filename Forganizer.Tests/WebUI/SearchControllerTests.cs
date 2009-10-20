using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Forganizer.DomainModel.Abstract;
using Forganizer.DomainModel.Entities;
using Forganizer.WebUI.Controllers;
using Forganizer.WebUI.Models;
using Moq;
using NUnit.Framework;

namespace Forganizer.Tests.WebUI
{
    [TestFixture]
    class SearchControllerTests
    {
        Mock<IFileObjectRepository> mockFileObjectRepository;
        Mock<ICategoryRepository> mockCategoryRepository;

        [SetUp]
        public void Setup()
        {
            List<FileObject> fobs = new List<FileObject>() {
                new FileObject() { Id=1, FilePath=@"C:\some_folder\another one\image.jpg", Active=true, TagString="funny big" },
                new FileObject() { Id=2, FilePath=@"C:\some_folder\second\another_image.jpg", Active=true, TagString="serious big" },
                new FileObject() { Id=3, FilePath=@"C:\some_folder\second\textfile.txt", Active=true, TagString="funny long" },
                new FileObject() { Id=4, FilePath=@"C:\some_folder\second\ninjas.txt", Active=false, TagString="turds" }
            };
            mockFileObjectRepository = new Mock<IFileObjectRepository>();
            mockFileObjectRepository.Setup(x => x.FileObjects).Returns(fobs.AsQueryable().Where(x => x.Active));
            mockFileObjectRepository.Setup(x => x.AllFileObjects).Returns(fobs.AsQueryable());

            List<Category> categories = new List<Category>() {
                new Category() { Id=1, Name="video", ExtensionString=".mpg .avi"},
                new Category() { Id=2, Name="image", ExtensionString=".jpg"},
                new Category() { Id=3, Name="text", ExtensionString=".doc txt"},
            };
            mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(x => x.Categories).Returns(categories.AsQueryable());
        }
        
        [Test]
        public void Index_presents_correct_FileAndTagCollection()
        {
            Assert.IsNotNull(mockFileObjectRepository.Object);
            Assert.IsNotNull(mockCategoryRepository.Object);
            Assert.AreEqual(3, mockCategoryRepository.Object.Categories.Count());

            SearchController controller = new SearchController(mockFileObjectRepository.Object, mockCategoryRepository.Object);

            ViewResult result = controller.Index("funny", ".jpg", 1, "And");
            FileAndTagCollection Model = result.ViewData.Model as FileAndTagCollection;
            
            Assert.AreEqual(".jpg", Model.Extensions.First().Name);
            Assert.AreEqual(2, Model.Extensions.Where(x => !x.Active).Count());
            Assert.AreEqual(1, Model.Extensions.Where(x => x.Active).Count());

            Assert.AreEqual(2, Model.Categories.Count());
            Assert.AreEqual(1, Model.Categories.Where(x => x.Active).Count());
            Assert.AreEqual(1, Model.Categories.Where(x => !x.Active).Count());

            Assert.AreEqual(2, Model.Tags.Count());
            Assert.AreEqual(2, Model.Tags.Where(x => x.Active).Count());
            Assert.AreEqual(0, Model.Tags.Where(x => !x.Active).Count());

            Assert.AreEqual(1, Model.PageOfFileObjects.Count());
            Assert.AreEqual("image.jpg", Model.PageOfFileObjects.First().FileInfo.Name);

            result = controller.Index("funny,long", "", 1, "Or");
            Model = result.ViewData.Model as FileAndTagCollection;
            Assert.AreEqual(5, Model.Tags.Count());
            Assert.AreEqual(3, Model.Tags.Where(x => x.Active).Count());
        }
    }
}

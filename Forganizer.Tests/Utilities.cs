using System.Linq;
using System.Web;
using System.Web.Routing;
using Forganizer.DomainModel.Abstract;
using Forganizer.DomainModel.Entities;
using Forganizer.WebUI;
using Moq;
using NUnit.Framework;

namespace Forganizer.Tests
{
    class Utilities
    {
        public class Routing
        {
            public static void TestInboundRoute(string url, object expectedValues)
            {
                RouteCollection routes = new RouteCollection();
                MvcApplication.RegisterRoutes(routes);
                var mockHttpContext = new Mock<HttpContextBase>();
                var mockRequest = new Mock<HttpRequestBase>();
                mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);
                mockRequest.Setup(x => x.AppRelativeCurrentExecutionFilePath).Returns(url);

                RouteData routeData = routes.GetRouteData(mockHttpContext.Object);

                Assert.IsNotNull(routeData);
                var expectedDict = new RouteValueDictionary(expectedValues);
                foreach (var expectedVal in expectedDict)
                {
                    if (expectedVal.Value == null) Assert.IsNull(routeData.Values[expectedVal.Key]);
                    else Assert.AreEqual(expectedVal.Value.ToString(), routeData.Values[expectedVal.Key].ToString());
                }
            }

            public static string GetOutboundUrl(object routeValues)
            {
                RouteCollection routes = new RouteCollection();
                MvcApplication.RegisterRoutes(routes);
                var mockHttpContext = new Mock<HttpContextBase>();
                var mockRequest = new Mock<HttpRequestBase>();
                var fakeResponse = new FakeResponse();
                mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);
                mockHttpContext.Setup(x => x.Response).Returns(fakeResponse);
                mockRequest.Setup(x => x.ApplicationPath).Returns("/");

                var ctx = new RequestContext(mockHttpContext.Object, new RouteData());
                return routes.GetVirtualPath(ctx, new RouteValueDictionary(routeValues)).VirtualPath;
            }

            private class FakeResponse : HttpResponseBase { public override string ApplyAppPathModifier(string x) { return x; } }
        }

        public class Mocking
        {
            public static Mock<IFileObjectRepository> MockFileObjectRepository(params FileObject[] fobs)
            {
                var mockFileObjectRepos = new Mock<IFileObjectRepository>();
                mockFileObjectRepos.Setup(x => x.FileObjects).Returns(fobs.AsQueryable());
                return mockFileObjectRepos;
            }

            public static Mock<IFileObjectRepository> MockFileObjectRepository()
            {
                FileObject[] fobs = new FileObject[] 
                { 
                    new FileObject() { Id=1, FilePath=@"C:\some_folder\another one\image.jpg", Active=true },
                    new FileObject() { Id=2, FilePath=@"C:\some_folder\second\another_image.jpg", Active=true },
                    new FileObject() { Id=3, FilePath=@"C:\some_folder\second\textfile.txt", Active=true },
                    
                    new FileObject() { Id=4, FilePath=@"C:\some_folder\second\another.txt", Active=false }
                };

                fobs[0].AddTags("funny");
                fobs[0].AddTags("big");
                fobs[1].AddTags("serious");
                fobs[1].AddTags("big");
                fobs[2].AddTags("funny");
                fobs[2].AddTags("long");

                fobs[3].AddTags("party");

                var mockFileObjectRepos = new Mock<IFileObjectRepository>();
                mockFileObjectRepos.Setup(x => x.FileObjects).Returns(fobs.AsQueryable().Where(x => x.Active));
                mockFileObjectRepos.Setup(x => x.AllFileObjects).Returns(fobs.AsQueryable());
                return mockFileObjectRepos;
            }
        }
    }
}

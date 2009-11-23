using System.Linq;
using System.Web;
using System.Web.Routing;
using Forganizer.DomainModel.Abstract;
using Forganizer.DomainModel.Entities;
using Forganizer.WebUI;
using Moq;
using System.Collections.Generic;
using NUnit.Framework;
using System.Web.Mvc;
using System.Collections.Specialized;

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

        public class ContextMocks
        {
            public Mock<HttpContextBase> HttpContext { get; private set; }
            public Mock<HttpRequestBase> Request { get; private set; }
            public Mock<HttpResponseBase> Response { get; private set; }
            public RouteData RouteData { get; private set; }

            public ContextMocks(Controller onController)
            {
                HttpContext = new Mock<HttpContextBase>();
                Request = new Mock<HttpRequestBase>();
                Response = new Mock<HttpResponseBase>();
                HttpContext.Setup(x => x.Request).Returns(Request.Object);
                HttpContext.Setup(x => x.Response).Returns(Response.Object);
                HttpContext.Setup(x => x.Session).Returns(new FakeSessionState());
                Request.Setup(x => x.Cookies).Returns(new HttpCookieCollection());
                Response.Setup(x => x.Cookies).Returns(new HttpCookieCollection());
                Request.Setup(x => x.QueryString).Returns(new NameValueCollection());
                Request.Setup(x => x.Form).Returns(new NameValueCollection());

                RequestContext rc = new RequestContext(HttpContext.Object, new RouteData());
                onController.ControllerContext = new ControllerContext(rc, onController);
            }

            private class FakeSessionState : HttpSessionStateBase
            {
                Dictionary<string, object> items = new Dictionary<string, object>();
                public override object this[string name]
                {
                    get { return items.ContainsKey(name) ? items[name] : null; }
                    set { items[name] = value; }
                }
            }
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

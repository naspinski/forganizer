﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using Forganizer.WebUI;
using Moq;
using System.Web;
using NUnit.Framework;
using Forganizer.DomainModel.Abstract;
using Forganizer.DomainModel.Entities;

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
            public static IFileObjectRepository MockFileObjectRepository(params FileObject[] fobs)
            {
                var mockFileObjectRepos = new Mock<IFileObjectRepository>();
                mockFileObjectRepos.Setup(x => x.FileObjects).Returns(fobs.AsQueryable());
                return mockFileObjectRepos.Object;
            }

            public static IFileObjectRepository MockFileObjectRepository()
            {
                FileObject[] fobs = new FileObject[] 
                { 
                    new FileObject() { FilePath=@"C:\some_folder\another one\image.jpg" },
                    new FileObject() { FilePath=@"C:\some_folder\second\another_image.jpg" },
                    new FileObject() { FilePath=@"C:\some_folder\second\textfile.txt" }
                };

                fobs[0].AddTag("funny");
                fobs[0].AddTag("big");
                fobs[1].AddTag("serious");
                fobs[1].AddTag("big");
                fobs[2].AddTag("funny");
                fobs[2].AddTag("long");

                var mockFileObjectRepos = new Mock<IFileObjectRepository>();
                mockFileObjectRepos.Setup(x => x.FileObjects).Returns(fobs.AsQueryable());
                return mockFileObjectRepos.Object;
            }
        }
    }
}

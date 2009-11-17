using System.Collections.Generic;
using Forganizer.DomainModel.Entities;
using System.Web.Routing;

namespace Forganizer.WebUI.Models
{
    public class FileAndTagCollection
    {
        public IEnumerable<FileObject> PageOfFileObjects { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<Tag> Extensions { get; set; }
        public IEnumerable<Tag> Categories { get; set; }
        public RouteData RouteData { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forganizer.DomainModel.Entities
{
    public class Category
    {
        public string Name { get; set; }
        public List<string> Extensions { get; set; }

        public Category()
        {
            Extensions = new List<string>();
        }

        public string ExtensionString
        {
            get
            {
                StringBuilder sv = new StringBuilder();
                for (int i = 0; i < Extensions.Count; i++)
                    sv.Append(Extensions[i] + (i == Extensions.Count - 1 ? "" : Constants.UrlDelimiter));
                return sv.ToString();
            }
        }
    }
}

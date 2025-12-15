using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blazor01
{
    public class Page
    {
        public string Url { get; private set; }
        public string Name { get; private set; }

        public string Html
        {
            get
            {
                return $"<a href=\"{this.Url}\">{this.Name}</a>";
            }
            private set { }
        }

        public Page(string url, string name)
        {
            Url = url;
            Name = name;
        }

        public Page() { }
    }
}
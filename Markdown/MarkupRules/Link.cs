using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown.MarkupRules
{
    internal class Link : IMarkupRule
    {
        public string MarkupTag { get; set; } = "[";
        public string HtmlTag { get; set; } = "a";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = true;
        public bool HasAttribute { get; } = true;
        public IEnumerable<TagAttribute> Attributes { get; set; }
        public string GeneratedBody { get; set; }
    }
}

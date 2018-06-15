using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown.MarkupRules
{
    internal class LineBreak : IMarkupRule
    {
        public string MarkupTag { get; } = "";
        public string HtmlTag { get; } = "br";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = false;
        public bool HasAttribute { get; } = false;
        public bool UseForBlockText { get; set; } = true;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; } = "";
    }
}

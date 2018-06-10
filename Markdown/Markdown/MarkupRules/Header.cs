using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    internal class Header : IMarkupRule
    {
        public string MarkupTag { get; } = "#";
        public string HtmlTag { get; } = "h1";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = true;        
    }
}

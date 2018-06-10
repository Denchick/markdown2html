using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    internal class Header5 : IMarkupRule
    {
        public string MarkupTag { get; } = "#####";
        public string HtmlTag { get; } = "h5";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = true;        
    }
}

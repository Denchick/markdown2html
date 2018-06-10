using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    internal class Header3 : IMarkupRule
    {
        public string MarkupTag { get; } = "###";
        public string HtmlTag { get; } = "h3";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = true;        
    }
}

using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    internal class Header4 : IMarkupRule
    {
        public string MarkupTag { get; } = "####";
        public string HtmlTag { get; } = "h4";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = true;
        public bool HasAttribute { get; } = false;
    }
}

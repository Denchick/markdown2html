using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    internal class Header6 : IMarkupRule
    {
        public string MarkupTag { get; } = "######";
        public string HtmlTag { get; } = "h6";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = true;
        public bool HasAttribute { get; } = false;
    }
}

using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    internal class Bold : IMarkupRule
    {
        public string MarkupTag { get; } = "__";
        public string HtmlTag { get; } = "strong";
        public bool HaveClosingMarkupTag { get; } = true;
        public bool HaveClosingHtmlTag { get; } = true;
    }
}

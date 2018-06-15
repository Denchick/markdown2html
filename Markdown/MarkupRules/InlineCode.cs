using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    public class InlineCode : IMarkupRule
    {
        public string MarkdownTag { get; } = "`";
        public string HtmlTag { get; } = "code";
        public bool HaveClosingMarkupTag { get; } = true;
        public bool HaveClosingHtmlTag { get; } = true;
        public bool HasAttribute { get; } = false;
        public bool UseForBlockText { get; set; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; } = "";
    }
}
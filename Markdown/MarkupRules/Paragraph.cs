using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    public class Paragraph : IMarkupRule
    {
        public string MarkupTag { get; } = "";
        public string HtmlTag { get; } = "p";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = true;
        public bool HasAttribute { get; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string GeneratedBody { get; } = "";
    }
}
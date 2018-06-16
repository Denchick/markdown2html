using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    internal class Quotation : IMarkupRule
    {
        public string MarkdownTag { get; } = ">";
        public string Tag { get; } = "blockquote";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingTag { get; } = true;
        public bool HasAttribute { get; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; } = "";
        public bool UseForMultiline { get; } = true;
    }
}

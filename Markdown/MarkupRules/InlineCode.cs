using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    public class InlineCode : IMarkupRule
    {
        public string MarkdownTag { get; } = "`";
        public string Tag { get; } = "code";
        public bool HaveClosingMarkupTag { get; } = true;
        public bool HaveClosingTag { get; } = true;
        public bool HasAttribute { get; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; } = "";
        public bool UseForMultiline { get; } = false;
    }
}
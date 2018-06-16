using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    public class Paragraph : IMarkupRule
    {
        public string MarkdownTag { get; } = "";
        public string Tag { get; } = "p";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingTag { get; } = true;
        public bool HasAttribute { get; } = false;
        public bool UseForBlockText { get; set; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; } = "";
        public bool UseForMultiline { get; } = false;
    }
}
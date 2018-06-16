using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    internal class Link : IMarkupRule
    {
        public string MarkdownTag { get; set; } = "[";
        public string Tag { get; set; } = "a";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingTag { get; } = true;
        public bool HasAttribute { get; } = true;
        public IEnumerable<TagAttribute> Attributes { get; set; }
        public string TextInsideTag { get; set; }
        public bool UseForMultiline { get; } = false;
    }
}

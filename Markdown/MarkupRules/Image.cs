using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    internal class ImageTag : IMarkupRule
    {
        public string MarkdownTag { get; set; } = "![";
        public string Tag { get; set; } = "img";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingTag { get; } = false;
        public bool HasAttribute { get; } = true;
        public IEnumerable<TagAttribute> Attributes { get; set; }
        public string TextInsideTag { get; }
        public bool UseForMultiline { get; } = false;
    }
}

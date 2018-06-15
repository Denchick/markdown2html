using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    internal class ImageTag : IMarkupRule
    {
        public string MarkdownTag { get; set; } = "![";
        public string HtmlTag { get; set; } = "img";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = false;
        public bool HasAttribute { get; } = true;
        public bool UseForBlockText { get; set; } = false;
        public IEnumerable<TagAttribute> Attributes { get; set; }
        public string TextInsideTag { get; }
    }
}

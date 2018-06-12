using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    internal class ImageTag : IMarkupRule
    {
        public string MarkupTag { get; set; } = "![";
        public string HtmlTag { get; set; } = "img";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = false;
        public bool HasAttribute { get; } = true;
        public IEnumerable<TagAttribute> Attributes { get; set; }
        public string GeneratedBody { get; }
    }
}

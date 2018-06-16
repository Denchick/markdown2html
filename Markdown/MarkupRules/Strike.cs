using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    internal class Strike : IMarkupRule
    {
        public string MarkdownTag => "~~";
        public string Tag { get; } = "s";
        public bool HaveClosingMarkupTag { get; } = true;
        public bool HaveClosingTag { get; } = true;
        public bool HasAttribute { get; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; } = "";
        public bool UseForMultiline { get; } = false;
    }
}

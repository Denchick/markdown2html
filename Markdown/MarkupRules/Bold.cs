using System;
using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    public  abstract class Bold
    {
        public string MarkdownTag => throw new NotImplementedException();
        public string Tag { get; } = "strong";
        public bool HaveClosingMarkupTag { get; } = true;
        public bool HaveClosingTag { get; } = true;
        public bool HasAttribute { get; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; } = "";
        public bool UseForMultiline { get; } = false;
    }

    public class BoldRuleWithDoubleAsterisks : Bold, IMarkupRule
    {
        public new string MarkdownTag => "**";
        
    }

    public class BoldRuleWithDoubleUnderscores : Bold, IMarkupRule
    {
        public new string MarkdownTag => "__";
    }
}

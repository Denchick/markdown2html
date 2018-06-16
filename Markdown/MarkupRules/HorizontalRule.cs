using System;
using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    abstract class HorizontalRule
    {
        public string MarkdownTag => throw new NotImplementedException();
        public string Tag { get; } = "hr";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingTag { get; } = false;
        public bool HasAttribute { get; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; } = "";
        public bool UseForMultiline { get; } = false;

    }

    internal class HorizontalRuleWith3Stars : HorizontalRule, IMarkupRule
    {
        public new string MarkdownTag => "***";
    }

    internal class HorizontalRuleWith3StarsSeparatedBySpaces : HorizontalRule, IMarkupRule
    {
        public new string MarkdownTag => "* * *";
        
    }

    internal class HorizontalRuleWith3Hyphens : HorizontalRule, IMarkupRule
    {
        public new string MarkdownTag => "---";
    }

    internal class HorizontalRuleWith3HyphensSeparatedBySpaces : HorizontalRule, IMarkupRule
    {
        public new string MarkdownTag => "- - -";
    }
}

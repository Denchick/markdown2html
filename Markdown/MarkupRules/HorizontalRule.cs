using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown.MarkupRules
{
    abstract class HorizontalRule
    {
        public string MarkupTag => throw new NotImplementedException();
        public string HtmlTag { get; } = "hr";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = false;
        public bool HasAttribute { get; } = false;
        public bool UseForBlockText { get; set; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string GeneratedBody { get; } = "";
        
    }

    internal class HorizontalRuleWith3Stars : HorizontalRule, IMarkupRule
    {
        public new string MarkupTag => "***";
    }

    internal class HorizontalRuleWith3StarsSeparatedBySpaces : HorizontalRule, IMarkupRule
    {
        public new string MarkupTag => "* * *";
        
    }

    internal class HorizontalRuleWith3Hyphens : HorizontalRule, IMarkupRule
    {
        public new string MarkupTag => "---";
    }

    internal class HorizontalRuleWith3HyphensSeparatedBySpaces : HorizontalRule, IMarkupRule
    {
        public new string MarkupTag => "- - -";
    }
}

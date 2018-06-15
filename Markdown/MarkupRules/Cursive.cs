using System;
using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    public abstract class Cursive
    {
        public string MarkdownTag => throw new NotImplementedException();
        public string HtmlTag { get; } = "em";
        public bool HaveClosingMarkupTag { get; } = true;
        public bool HaveClosingHtmlTag { get; } = true;
        public bool HasAttribute { get; } = false;
        public bool UseForBlockText { get; set; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; } = "";
    }

    public class CursiveRuleWithSingleUnderscores : Cursive, IMarkupRule
    {
        public new string MarkdownTag => "_";
    }

    public class CursiveRuleWithSingleAsterisks : Cursive, IMarkupRule
    {
        public new string MarkdownTag => "*";
    }
}

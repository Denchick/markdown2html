using System;
using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    public abstract class Cursive
    {
        public string MarkdownTag => throw new NotImplementedException();
        public string Tag { get; } = "em";
        public bool HaveClosingMarkupTag { get; } = true;
        public bool HaveClosingTag { get; } = true;
        public bool HasAttribute { get; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; } = "";
        public bool UseForMultiline { get; } = false;
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

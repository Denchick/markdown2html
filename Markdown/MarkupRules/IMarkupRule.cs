using System;
using System.Collections.Generic;
using Markdown.MarkupRules;

namespace Markdown
{
    public interface IMarkupRule
    {
        string MarkdownTag { get; }
        string HtmlTag { get; }
        bool HaveClosingMarkupTag { get; }
        bool HaveClosingHtmlTag { get; }
        bool HasAttribute { get; }
        bool UseForBlockText { get; set; }
        IEnumerable<TagAttribute> Attributes { get; }
        string TextInsideTag { get; }
    }
}

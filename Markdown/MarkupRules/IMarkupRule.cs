using System;
using System.Collections.Generic;
using Markdown.MarkupRules;

namespace Markdown
{
    public interface IMarkupRule
    {
        string MarkupTag { get; }
        string HtmlTag { get; }
        bool HaveClosingMarkupTag { get; }
        bool HaveClosingHtmlTag { get; }
        bool HasAttribute { get; }
        IEnumerable<TagAttribute> Attributes { get; }
        string GeneratedBody { get; }
    }
}

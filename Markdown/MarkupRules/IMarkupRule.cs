using System.Collections.Generic;
using Markdown.MarkupRules;

namespace Markdown
{
    public interface IMarkupRule : IFormatToConvert
    {
        string MarkdownTag { get; }
        bool HaveClosingMarkupTag { get; }
        bool UseForMultiline { get; }
    }

    public interface IFormatToConvert
    {
        string Tag { get; }
        bool HaveClosingTag { get; }
        IEnumerable<TagAttribute> Attributes { get; }
        string TextInsideTag { get; }
        bool HasAttribute { get; }
    }
}

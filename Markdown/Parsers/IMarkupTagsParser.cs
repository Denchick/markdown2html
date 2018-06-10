using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    public interface IMarkupTagsParser
    {
        IEnumerable<ParsedSubline> ParseLine(string line);
    }
}
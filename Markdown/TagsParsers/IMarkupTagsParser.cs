using System.Collections.Generic;

namespace Markdown.TagsParsers
{
    public interface IMarkupTagsParser
    {
        IEnumerable<ParsedSubline> ParseLine(string line);
        IEnumerable<ParsedSubline> ParseMultilineText(string multilineText);
        bool UseParserForBlockText { get; }
    }
}
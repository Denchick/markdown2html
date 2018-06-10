using System.Collections.Generic;

namespace Markdown.TagsParsers
{
    public interface IMarkupTagsParser
    {
        IEnumerable<ParsedSubline> ParseLine(string line);
    }
}
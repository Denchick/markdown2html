using System.Collections.Generic;

namespace Markdown.TagsParsers
{
    public interface IInLineParser : IParser
    {
        IEnumerable<Token> ParseLine(string line);
    }
}

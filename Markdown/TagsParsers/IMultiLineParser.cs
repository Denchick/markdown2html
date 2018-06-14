using System.Collections.Generic;

namespace Markdown.TagsParsers
{
    public interface IMultiLineParser : IParser
    {
        IEnumerable<Token> ParseMultilineText(string multilineText);
    }
}
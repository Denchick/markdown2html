using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown.TagsParsers
{
    public interface IInLineParser : IParser
    {
        IEnumerable<Token> ParseLine(string line);
    }
}

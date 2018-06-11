using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown.TagsParsers
{
    class MultilineCodeTagsParser : IMarkupTagsParser
    {
        public List<IMarkupRule> CurrentMarkupRules { get; }

        public MultilineCodeTagsParser(List<IMarkupRule> currentMarkupRules)
        {
            CurrentMarkupRules = currentMarkupRules
                .Where(e => e.HaveClosingMarkupTag)
                .OrderByDescending(e => e.MarkupTag.Length)
                .ToList();
        }

        public IEnumerable<ParsedSubline> ParseLine(string line)
        {
            throw new NotImplementedException();
        }
    }
}

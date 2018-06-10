using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Markdown.MarkupRules;

namespace Markdown.Parsers
{
    public class SingleMarkupTagsParser : IMarkupTagsParser
    {
        private List<IMarkupRule> CurrentMarkupRules { get; }

        public SingleMarkupTagsParser(IEnumerable<IMarkupRule> currentMarkupRules)
        {
            CurrentMarkupRules = currentMarkupRules
                .Where(e => !e.HaveClosingMarkupTag && !(e is Paragraph))
                .OrderByDescending(e => e.MarkupTag.Length)
                .ToList();
        }

        public IEnumerable<ParsedSubline> ParseLine(string line)
        {
            foreach (var currentMarkupRule in CurrentMarkupRules)
            {
                if (!line.StartsWith(currentMarkupRule.MarkupTag)) continue;
                if (line.EndsWith(currentMarkupRule.MarkupTag))
                    return new List<ParsedSubline>()
                    {
                        new ParsedSubline(0, line.Length - currentMarkupRule.MarkupTag.Length, currentMarkupRule)
                    };
                return new List<ParsedSubline>()
                {
                    new ParsedSubline(0, line.Length, currentMarkupRule)
                };
            }
            return new List<ParsedSubline>();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Markdown.MarkupRules;

namespace Markdown.TagsParsers
{
    public class SingleMarkupTagsParser : IMarkupTagsParser
    {
        private List<IMarkupRule> CurrentMarkupRules { get; }

        public SingleMarkupTagsParser(IEnumerable<IMarkupRule> currentMarkupRules)
        {
            CurrentMarkupRules = currentMarkupRules
                .Where(e => !e.HaveClosingMarkupTag && !(e is Paragraph) && !e.HasAttribute && !e.UseForBlockText)
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

        public IEnumerable<ParsedSubline> ParseMultilineText(string multilineText)
        {
            return new List<ParsedSubline>();
        }

        public bool UseParserForBlockText { get; } = false;
    }
}
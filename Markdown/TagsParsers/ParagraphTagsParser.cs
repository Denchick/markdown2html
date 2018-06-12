using System.Collections.Generic;
using System.Linq;
using Markdown.MarkupRules;
using NUnit.Framework.Internal;

namespace Markdown.TagsParsers
{
    public class ParagraphTagsParser : IMarkupTagsParser
    {
        private List<IMarkupRule> CurrentMarkupRules { get; }

        public ParagraphTagsParser(IEnumerable<IMarkupRule> currentMarkupRules)
        {
            CurrentMarkupRules = currentMarkupRules
                .Where(e => !e.HaveClosingMarkupTag && !(e is Paragraph) && !e.UseForBlockText)
                .OrderByDescending(e => e.MarkupTag.Length)
                .ToList();
        }

        public IEnumerable<ParsedSubline> ParseLine(string line)
        {
            var result = new List<ParsedSubline>();
            if (!CurrentMarkupRules.Any(e => line.StartsWith(e.MarkupTag)))
                result.Add(new ParsedSubline(-1, line.Length, new Paragraph()));
            return result;            
        }

        public IEnumerable<ParsedSubline> ParseMultilineText(string multilineText)
        {
            return new List<ParsedSubline>();
        }

        public bool UseParserForBlockText { get; } = false;
    }
}
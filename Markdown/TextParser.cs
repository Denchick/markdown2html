using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Markdown.Parsers;
using NUnit.Framework;

namespace Markdown
{
    public class TextParser
    {
        private List<IMarkupRule> CurrentMarkupRules { get; }
        public TextParser(IEnumerable<IMarkupRule> rules)
        {
            CurrentMarkupRules = rules
                .OrderByDescending(e => e.MarkupTag.Length)
                .ToList();
        }

        public IEnumerable<ParsedSubline> ParseLine(string line)
        {
            var result = new List<ParsedSubline>();
            if (line is null) return result;

            var currentLine = line;
            var singleTagsParser = new SingleMarkupTagsParser(CurrentMarkupRules);
            var pairTagsParser = new PairedMarkupTagParser(CurrentMarkupRules);
            var paragraphsParser = new ParagraphTagsParser(CurrentMarkupRules);
            
            result.AddRange(singleTagsParser.ParseLine(line));
            result.AddRange(pairTagsParser.ParseLine(line));
            result.AddRange(paragraphsParser.ParseLine(line));
            
            return result;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Markdown.MarkupRules;
using Markdown.TagsParsers;
using NUnit.Framework;

namespace Markdown
{
    public class TextParser
    {
        private List<IMarkupRule> CurrentMarkupRules { get; }
        private List<IMarkupTagsParser> CurrentTagsParsers { get; }

        public TextParser(IEnumerable<IMarkupRule> rules, IEnumerable<IMarkupTagsParser> parsers)
        {
            CurrentMarkupRules = rules
                .OrderByDescending(e => e.MarkupTag.Length)
                .ToList();
            CurrentTagsParsers = parsers.ToList();
        }

        public IEnumerable<ParsedSubline> ParseLine(string line)
        {
            var result = new List<ParsedSubline>();
            if (line is null) return result;
            
            foreach (var parser in CurrentTagsParsers)
                result.AddRange(parser.ParseLine(line));
            
            return result;
        }
    }
}
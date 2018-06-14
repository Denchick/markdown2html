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
        private List<IInLineParser> InLineParsers { get; }
        private List<IMultiLineParser> MultiLineParser { get; }

        public TextParser(IEnumerable<IMarkupRule> rules, IEnumerable<IParser> parsers)
        {
            CurrentMarkupRules = rules
                .OrderByDescending(e => e.MarkupTag.Length)
                .ToList();

            InLineParsers = parsers.Select(e => e as IInLineParser).Where(e => e != null).ToList();
            MultiLineParser = parsers.Select(e => e as IMultiLineParser).Where(e => e != null).ToList();   
        }


        public IEnumerable<Token> ParseLine(string line)
        {
            var result = new List<Token>();
            if (line is null) return result;
            
            foreach (var parser in InLineParsers)
                result.AddRange(parser.ParseLine(line));
            
            return result;
        }

        public IEnumerable<Token> ParseMultilineText(string line)
        {
            var result = new List<Token>();
            if (line is null) return result;

            foreach (var parser in MultiLineParser)
                result.AddRange(parser.ParseMultilineText(line));

            return result;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Markdown.MarkupRules;
using NUnit.Framework.Internal;

namespace Markdown.TagsParsers
{
    public class ParagraphTagsParser : IInLineParser
    {
        private List<IMarkupRule> CurrentMarkupRules { get; }

        public ParagraphTagsParser(IEnumerable<IMarkupRule> currentMarkupRules)
        {
            CurrentMarkupRules = currentMarkupRules
                .Where(e => !e.HaveClosingMarkupTag && !(e is Paragraph) && !e.UseForBlockText)
                .OrderByDescending(e => e.MarkupTag.Length)
                .ToList();
        }

        public IEnumerable<Token> ParseLine(string line)
        {
            var result = new List<Token>();

            if (CurrentMarkupRules.Any(e => line.StartsWith(e.MarkupTag))) return result;
            var endTag = line.Length;
            var startTag = -1;
            if (line.EndsWith("</blockquote>"))
                endTag = endTag - "</blockquote>".Length;
            if (line.StartsWith("<blockquote>"))
                startTag = "<blockquote>".Length;
            result.Add(new Token(startTag, endTag, new Paragraph()));

            return result;            
        }

        public IEnumerable<Token> ParseMultilineText(string multilineText)
        {
            return new List<Token>();
        }

        public bool UseParserForBlockText { get; } = false;
    }
}
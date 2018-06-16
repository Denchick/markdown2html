using System.Collections.Generic;
using System.Linq;
using Markdown.MarkupRules;

namespace Markdown.TagsParsers
{
    public class SingleMarkupTagsParser : IInLineParser
    {
        private List<IMarkupRule> CurrentMarkupRules { get; }

        public SingleMarkupTagsParser(IEnumerable<IMarkupRule> currentMarkupRules)
        {
            CurrentMarkupRules = currentMarkupRules
                .Where(e => !e.HaveClosingMarkupTag && !(e is Paragraph) && !e.HasAttribute && !e.UseForMultiline)
                .OrderByDescending(e => e.MarkdownTag.Length)
                .ToList();
        }

        public IEnumerable<Token> ParseLine(string line)
        {
            foreach (var currentMarkupRule in CurrentMarkupRules)
            {
                if (!line.StartsWith(currentMarkupRule.MarkdownTag)) continue;
                if (line.EndsWith(currentMarkupRule.MarkdownTag))
                    return new List<Token>()
                    {
                        new Token(0, line.Length - currentMarkupRule.MarkdownTag.Length, currentMarkupRule)
                    };
                return new List<Token>()
                {
                    new Token(0, line.Length, currentMarkupRule)
                };
            }
            return new List<Token>();
        }

        public IEnumerable<Token> ParseMultilineText(string multilineText)
        {
            return new List<Token>();
        }

        public bool UseParserForBlockText { get; } = false;
    }
}
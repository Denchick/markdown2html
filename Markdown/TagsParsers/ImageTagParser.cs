using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Markdown.MarkupRules;
using Markdown.TagsParsers;

namespace Markdown.Parsers
{
    internal class ImageTagParser : IInLineParser
    {

        private List<IMarkupRule> CurrentMarkupRules { get; }
        private readonly Regex getFullMarkdownTag = new Regex("!\\[(.*)\\]\\((.*)\\)");
        public ImageTagParser(IEnumerable<IMarkupRule> currentMarkupRules)
        {
            CurrentMarkupRules = currentMarkupRules
                .Where(e => !e.HaveClosingMarkupTag && e.HasAttribute)
                .OrderByDescending(e => e.MarkdownTag.Length)
                .ToList();
        }   

        public IEnumerable<Token> ParseLine(string line)
        {
            var result = new List<Token>();

            for (var i = 0; i < line.Length; i++)
            {
                if (DetermineRuleOfSubline(line, i) is null)
                    continue;
                var tag = getFullMarkdownTag.Match(line.Substring(i));
                if (tag.Length == 0)
                    continue;
                var src = new TagAttribute(tag.Groups[2].Value, "src");
                var alt = new TagAttribute(tag.Groups[1].Value, "alt");
                var attributes = new List<TagAttribute>(){src, alt};
                var newTag = new ImageTag
                {
                    Attributes = attributes,
                    MarkdownTag = tag.Groups[0].Value
                };
                result.Add(new Token(i, newTag));
                i += tag.Length;
            }

            return result;
        }

        public IEnumerable<Token> ParseMultilineText(string multilineText)
        {
            return new List<Token>();
        }

        public bool UseParserForBlockText { get; } = false;

        private IMarkupRule DetermineRuleOfSubline(string line, int i)
        {

            return CurrentMarkupRules
                .Where(rule => i + rule.MarkdownTag.Length <= line.Length)
                .FirstOrDefault(rule => line.Substring(i, rule.MarkdownTag.Length) == rule.MarkdownTag);
        }
    }
}

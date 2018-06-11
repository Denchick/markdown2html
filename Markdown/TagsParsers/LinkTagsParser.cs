using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Markdown.MarkupRules;

namespace Markdown.TagsParsers
{
    internal class LinkTagsParser : IMarkupTagsParser
    {
        private IEnumerable<IMarkupRule> CurrentMarkupRules;
        private Regex getMarkdownTag = new Regex("[^!](\\[(.*?)\\]\\(([\\S]*)(.*?)\\))");

        public LinkTagsParser(IEnumerable<IMarkupRule> rules)
        {
            CurrentMarkupRules = rules.Where(r => r.HasAttribute && r.HaveClosingHtmlTag);
        }

        public IEnumerable<ParsedSubline> ParseLine(string line)
        {
            var result = new List<ParsedSubline>();
            for (int i = 0; i < line.Length; i++)
            {
                if (DetermineRuleOfSubline(line, i) == null)
                    continue;
                var offset = (i == 0) ? 0 : 1;
                var subLine = line.Substring(i - offset);
                var markdownTag = getMarkdownTag.Match(subLine);
                if (markdownTag.Length == 0 || !subLine.StartsWith(markdownTag.Value))
                    continue;
                var text = markdownTag.Groups[2].Value;
                var linkAttribute = new TagAttribute($"\"{markdownTag.Groups[3].Value}\"", "href"); ;
                var titleAttribute = new TagAttribute(markdownTag.Groups[4].Value, "title");
                var attributes = new List<TagAttribute> {linkAttribute, titleAttribute};
                var linkTag = new Link(){MarkupTag = markdownTag.Groups[1].Value, Attributes = attributes, GeneratedBody = text};
                result.Add(new ParsedSubline(i, i + markdownTag.Groups[1].Value.Length, linkTag));
                i += markdownTag.Groups[1].Length;
            }

            return result;
        }

        private IMarkupRule DetermineRuleOfSubline(string line, int i)
        {

            return CurrentMarkupRules
                .Where(rule => i + rule.MarkupTag.Length <= line.Length)
                .FirstOrDefault(rule => line.Substring(i, rule.MarkupTag.Length) == rule.MarkupTag);
        }
    }
}

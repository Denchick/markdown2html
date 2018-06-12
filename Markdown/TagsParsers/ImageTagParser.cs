﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Markdown.MarkupRules;
using Markdown.TagsParsers;

namespace Markdown.Parsers
{
    internal class ImageTagParser : IMarkupTagsParser
    {

        private List<IMarkupRule> CurrentMarkupRules { get; }
        private readonly Regex getFullMarkdownTag = new Regex("!\\[(.*)\\]\\((.*)\\)");
        public ImageTagParser(IEnumerable<IMarkupRule> currentMarkupRules)
        {
            CurrentMarkupRules = currentMarkupRules
                .Where(e => !e.HaveClosingMarkupTag && e.HasAttribute)
                .OrderByDescending(e => e.MarkupTag.Length)
                .ToList();
        }   

        public IEnumerable<ParsedSubline> ParseLine(string line)
        {
            var result = new List<ParsedSubline>();

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
                    MarkupTag = tag.Groups[0].Value
                };
                result.Add(new ParsedSubline(i, newTag));
                i += tag.Length;
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

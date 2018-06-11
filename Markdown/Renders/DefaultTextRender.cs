using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Markdown.Renders;
using Markdown.TagsParsers;
using static System.ValueTuple;

namespace Markdown
{
    public class DefaultTextRender : ITextRender
    {
        private List<IMarkupRule> CurrentMarkupRules { get; }
        private List<IMarkupTagsParser> CurrentTagsParsers { get; }

        public DefaultTextRender(IEnumerable<IMarkupRule> rules, IEnumerable<IMarkupTagsParser> parsers)
        {
            CurrentMarkupRules = rules
                .Where(e => e?.MarkupTag != null)
                .OrderBy(e => e.MarkupTag.Length)
                .ToList();
            CurrentTagsParsers = parsers.ToList();
        }

        public string RenderToHtml(string markdown)
        {
            var result = new StringBuilder();
            var parser = new TextParser(CurrentMarkupRules, CurrentTagsParsers);
            var render = new DefaultTextRender(CurrentMarkupRules, CurrentTagsParsers);
            foreach (var line in markdown.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                var parsed = parser.ParseLine(line);
                var rendered = render.RenderLine(line, parsed);
                result.Append($"{rendered}\n");
            }
            return result.ToString();
        }

        public string RenderLine(string line, IEnumerable<ParsedSubline> parsed)
        {
            var indexAndTagValueTuples = GetHtmlTagsOrderedByIndex(parsed);
            var offsetAfterReplacingTags = 0;
            var result = new StringBuilder(line);
            
            foreach (var (index, fromMarkupTagToHtml) in indexAndTagValueTuples)
            {
                var tag = GetHtmlTagFromMarkup(fromMarkupTagToHtml);

                if (index == line.Length)
                {
                    result.Append(tag);
                    continue;
                }
                
                var startIndex = Math.Max(index + offsetAfterReplacingTags, 0);

                if (startIndex - 1 > 0 && result[startIndex - 1] == '\\') continue;
                
                var markupTagLenght = fromMarkupTagToHtml.LenghtOfReplacedMarkupTag;
                
                result.Remove(startIndex, markupTagLenght);
                result.Insert(startIndex, tag);
                offsetAfterReplacingTags += tag.Length - markupTagLenght;   
            }
            return result.ToString();
        }

        private string GetHtmlTagFromMarkup(FromMarkupTagToHtml obj)
        {
            var markupRule = CurrentMarkupRules
                .FirstOrDefault(e => e.HtmlTag == obj.TagName);
            var attributes = "";
            if(obj.Rule.HasAttribute && !obj.IsClosingHtmlTag)
                attributes = string.Join(" ", obj.Rule.Attributes.Select(atr => $"{atr.Name}={atr.Value}").ToArray());
            return obj.IsClosingHtmlTag ? $@"</{obj.TagName}>" : $"<{obj.TagName} {attributes}>{obj.Rule.GeneratedBody}";
        }

        private static IEnumerable<(int, FromMarkupTagToHtml)> GetHtmlTagsOrderedByIndex(IEnumerable<ParsedSubline> parsed)
        {
            var insertedTags = new List<(int, FromMarkupTagToHtml)>();
            
            foreach (var subline in parsed)
            {
                var htmlTag = subline.MarkupRule.HtmlTag;
                var lenght = subline.MarkupRule.MarkupTag.Length;
                insertedTags.Add(
                    (subline.LeftBorderOfSubline, new FromMarkupTagToHtml(htmlTag, false, lenght, subline.MarkupRule)));
                if (subline.MarkupRule.HaveClosingHtmlTag)
                {
                    if (!subline.MarkupRule.HaveClosingMarkupTag)
                        lenght = 0;
                    insertedTags.Add(
                        (subline.RightBorderOfSubline, new FromMarkupTagToHtml(htmlTag, true, lenght,
                            subline.MarkupRule)));
                }
            }
            
            return insertedTags
                .OrderBy(e => e.Item1)
                .ToList();
        }
    }
}
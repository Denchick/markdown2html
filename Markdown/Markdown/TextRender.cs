using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.ValueTuple;

namespace Markdown
{
    public class TextRender
    {
        private List<IMarkupRule> CurrentMarkupRules { get; }

        public TextRender(List<IMarkupRule> rules)
        {
            CurrentMarkupRules = rules;
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
            return obj.IsClosingHtmlTag ? $@"</{obj.TagName}>" : $"<{obj.TagName}>";
        }

        private static IEnumerable<(int, FromMarkupTagToHtml)> GetHtmlTagsOrderedByIndex(IEnumerable<ParsedSubline> parsed)
        {
            var insertedTags = new List<(int, FromMarkupTagToHtml)>();
            
            foreach (var subline in parsed)
            {
                var htmlTag = subline.MarkupRule.HtmlTag;
                var lenght = subline.MarkupRule.MarkupTag.Length;
                insertedTags.Add(
                    (subline.LeftBorderOfSubline, new FromMarkupTagToHtml(htmlTag, false, lenght)));
                insertedTags.Add(
                    (subline.RightBorderOfSubline, new FromMarkupTagToHtml(htmlTag, true, lenght)));
            }
            
            return insertedTags
                .OrderBy(e => e.Item1)
                .ToList();
        }
    }
}
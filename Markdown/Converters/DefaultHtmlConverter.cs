using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Markdown.MarkupRules;
using Markdown.Renders;
using Markdown.TagsParsers;

namespace Markdown
{
    public class DefaultHtmlConverter : ILanguageConverter
    {
        private List<IMarkupRule> CurrentMarkupRules { get; }
        private List<IParser> CurrentTagsParsers { get; }

        public DefaultHtmlConverter(IEnumerable<IMarkupRule> rules, IEnumerable<IParser> parsers)
        {
            CurrentMarkupRules = rules
                .Where(e => e?.MarkdownTag != null)
                .OrderBy(e => e.MarkdownTag.Length)
                .ToList();
            CurrentTagsParsers = parsers.ToList();
        }

        public string ConvertToFormat(string markdown)
        {
            var result = new List<string>()
            {
                "<!doctype html>",
                "<html>",
                "<head>",
                "<meta charset='UTF-8'><meta name='viewport' content='width=device-width initial-scale=1'>",
                "</head>",
                "<body>",
                GetTextInHtml(markdown),
                "</body>",
                "</html>",
            };
            return string.Join("\r\n", result);
        }

        public string GetTextInHtml(string markdown)
        {
            var parser = new TextParser(CurrentMarkupRules, CurrentTagsParsers);
            var multilineParsed = ConvertByMultilinesParsers(markdown, parser).ToString();
            var inlineParsed = ConvertByInlinesParsers(multilineParsed, parser).ToString();
            return inlineParsed;
        }

        private string ReplaceAngleBrackets(string text, IEnumerable<Token> parsed)
        {
            var result = new StringBuilder(text);
            var leftAngleBracket = "&lt;";
            var rightAngleBracket = "&gt;";
            var offset = 0;
            foreach (var token in parsed)
            {
                if (!(token.MarkupRule is MultilineCode)) continue;
                for (int i = token.LeftBorderOfSubline + +token.MarkupRule.Tag.Length + 2; i <= token.RightBorderOfSubline + offset ; i++)
                {
                    if (result[i] == '<')
                    {
                        result.Remove(i, 1);
                        result.Insert(i, leftAngleBracket);
                        offset += leftAngleBracket.Length - 1;
                    }
                    else if (result[i] == '>')
                    {
                        result.Remove(i, 1);
                        result.Insert(i, rightAngleBracket);
                        offset += rightAngleBracket.Length - 1;
                    }
                }
            }
            return result.ToString();
        }

        private string RemoveBlockquoteMarkDownTag(string line)
        {
            if (line.StartsWith(">"))
            {
                line = line.TrimStart('>', ' ');
            }

            return line;
        }

        private StringBuilder ConvertByInlinesParsers(string markdownText, TextParser parser)
        {
            var result = new StringBuilder();
            foreach (var line in markdownText.Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries))
            {
                var lineWithoutBlockQuoteTag = RemoveBlockquoteMarkDownTag(line);
                var parsed = parser.ParseLine(lineWithoutBlockQuoteTag);
                var rendered = RenderLine(lineWithoutBlockQuoteTag, parsed);
                result.Append($"{rendered}\r\n");
            }
            return result;
        }

        private StringBuilder ConvertByMultilinesParsers(string markdownText, TextParser parser)
        {
            var result = new StringBuilder();
            foreach (var line in markdownText.Split(new string[] {"\r\n\r\n"}, StringSplitOptions.RemoveEmptyEntries))
            {
                var parsed = parser.ParseMultilineText(line);
                var rendered = RenderLine(line, parsed);
                result.Append($"{ReplaceAngleBrackets(rendered, parsed)}\r\n\r\n");
            }
            return result;
        }

        public string RenderLine(string line, IEnumerable<Token> parsed)
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
                .FirstOrDefault(e => e.Tag == obj.TagName);
            var attributes = "";
            if(obj.Rule.HasAttribute && !obj.IsClosingHtmlTag)
                attributes = string.Join("", obj.Rule.Attributes.Select(atr => $" {atr.Name}=\"{atr.Value}\"").ToArray());
            return obj.IsClosingHtmlTag ? $@"</{obj.TagName}>" : $"<{obj.TagName}{attributes}>{obj.Rule.TextInsideTag}";
        }

        private static IEnumerable<(int, FromMarkupTagToHtml)> GetHtmlTagsOrderedByIndex(IEnumerable<Token> parsed)
        {
            var insertedTags = new List<(int, FromMarkupTagToHtml)>();
            
            foreach (var subline in parsed)
            {
                var htmlTag = subline.MarkupRule.Tag;
                var lenght = subline.MarkupRule.MarkdownTag.Length;
                insertedTags.Add(
                    (subline.LeftBorderOfSubline, new FromMarkupTagToHtml(htmlTag, false, lenght, subline.MarkupRule)));
                if (subline.MarkupRule.HaveClosingTag)
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
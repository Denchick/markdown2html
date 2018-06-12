using System.Collections.Generic;
using Markdown.MarkupRules;

namespace Markdown.TagsParsers
{
    class MultilineCodeTagsParser : IMarkupTagsParser
    {
        private readonly IMarkupRule rule;

        public MultilineCodeTagsParser()
        {
            rule = new MultilineCode();
        }

        public IEnumerable<ParsedSubline> ParseMultilineText(string multilineText)
        {
            var result = new List<ParsedSubline>();
            
            var offset = 0;
            while (multilineText.Length != 0)
            {
                var beginingTag = multilineText.IndexOf(rule.MarkupTag);
                if (beginingTag == -1)
                    return result;
                if (beginingTag != 0 && multilineText[beginingTag - 1] != '\n')
                {
                    multilineText = multilineText.Substring(beginingTag);
                    offset += beginingTag;
                    continue;
                }
                var finishTag = multilineText.Substring(beginingTag + rule.MarkupTag.Length).IndexOf(rule.MarkupTag);
                if (finishTag == -1)
                    return result;
                result.Add(new ParsedSubline(beginingTag + offset, finishTag + offset + rule.MarkupTag.Length, rule));
                multilineText = multilineText.Substring(finishTag + rule.MarkupTag.Length-1);
                offset += finishTag;
            }


            return result;
        }

        public IEnumerable<ParsedSubline> ParseLine(string multilineText)
        {
            return new List<ParsedSubline>();
        }

        public bool UseParserForBlockText { get; } = true;
    }
}

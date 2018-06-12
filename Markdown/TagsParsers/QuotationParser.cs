using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Markdown.MarkupRules;

namespace Markdown.TagsParsers
{
    internal class QuotationParser : IMarkupTagsParser
    {
        private readonly IMarkupRule quotationRule;

        public QuotationParser()
        {
            quotationRule = new Quotation();
        }
        public IEnumerable<ParsedSubline> ParseLine(string line)
        {
            return new List<ParsedSubline>();
        }

        private int GetNesting(string line)
        {
            var nesting = 0;
            foreach (var symbol in line)
            {
                if (symbol.ToString() == quotationRule.MarkupTag)
                {
                    nesting++;
                    continue;
                }
                if (symbol == ' ')
                    continue;
                return nesting;
            }
            return nesting;
        }

        public IEnumerable<ParsedSubline> ParseMultilineText(string multilineText)
        {
            var lineSeparatorLength = "\r\n".Length;
            var result = new List<ParsedSubline>();
            var lines = multilineText.Split(new[] {"\r\n"}, StringSplitOptions.None);
            var endIndex = 0;
            for (var i = 0; i < lines.Length; i++)
                if (lines[i].StartsWith(quotationRule.MarkupTag))
                {
                    var beginIndex = endIndex;
                    endIndex += lines[i].Length;
                    var indexOffset = i;
                    for (var j = i + 1; j < lines.Length; j++)
                    {
                        if (lines[j].StartsWith(quotationRule.MarkupTag)) break;
                        indexOffset = j;
                        endIndex += lines[j].Length  + lineSeparatorLength;
                    }

                    for (int j = 0; j < GetNesting(lines[i]); j++)
                    {
                        result.Add(new ParsedSubline(beginIndex + j, endIndex, quotationRule));
                    }
                    endIndex += lineSeparatorLength;
                    i = indexOffset;
                }

            return result;
        }

        public bool UseParserForBlockText { get; } = true;
    }
}

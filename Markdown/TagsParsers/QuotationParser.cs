using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Markdown.MarkupRules;

namespace Markdown.TagsParsers
{
    internal class QuotationParser : IMultiLineParser
    {
        private readonly IMarkupRule quotationRule;

        public QuotationParser()
        {
            quotationRule = new Quotation();
        }

        private int GetQuoteNesting(string line)
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

        public IEnumerable<Token> ParseLine(string line)
        {
            return new List<Token>();
        }

        public IEnumerable<Token> ParseMultilineText(string multilineText)
        {
            var lineSeparatorLength = "\r\n".Length;
            var result = new List<Token>();
            var lines = multilineText.Split(new[] {"\r\n"}, StringSplitOptions.None);
            var endIndex = 0;
            for (var i = 0; i < lines.Length; i++)
                if (lines[i].StartsWith(quotationRule.MarkupTag))
                {
                    var offset = endIndex + lines[i].Length + lineSeparatorLength;
                    var beginIndex = endIndex;
                    for (int j = i + 1; j < lines.Length; j++)
                    {
                        var nesting = GetQuoteNesting(lines[j]);
                        if (nesting > 1)
                        {
                            for (int k = 0; k < nesting - 1; k++)
                            {
                                result.Add(new Token(offset + k + 1, offset + lines[j].Length, new Quotation()));
                            }
                        }
                        offset += lines[j].Length + lineSeparatorLength;
                    }
                    result.Add(new Token(beginIndex, multilineText.Length, quotationRule));
                    return result;
                }
                else
                {
                    endIndex += lines[i].Length + lineSeparatorLength;
                }
            return result;
        }
    }


}


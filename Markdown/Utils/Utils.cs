using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Markdown.MarkupRules;
using Markdown.Parsers;
using Markdown.TagsParsers;

namespace Markdown
{
    public static class Utils
    {
        public static bool CanBeClosingTag(string line, int index, int tagLenght)
        {
            return index == line.Length - tagLenght ||
                   char.IsPunctuation(Convert.ToChar(line[index + tagLenght])) ||
                   char.IsWhiteSpace(Convert.ToChar(line[index + tagLenght]));
        }

        public static bool CanBeOpenningTag(string line, int index)
        {
            return index == 0 || char.IsPunctuation(Convert.ToChar(line[index - 1])) ||
                   char.IsWhiteSpace(Convert.ToChar(line[index - 1]));
        }

        //костыль
        public static IEnumerable<IMarkupRule> GetAllAvailableRules()
        {
            return new List<IMarkupRule>()
            {
                new Bold(), new Cursive(),
                new Header6(), new Header5(), new Header4(), new Header3(), new Header2(), new Headers(),
                new Paragraph(), new Code(), 
                new HorizontalRuleWith3Hyphens(), new HorizontalRuleWith3HyphensSeparatedBySpaces(),
                new HorizontalRuleWith3Stars(), new HorizontalRuleWith3StarsSeparatedBySpaces(),
                new ImageTag(), new Link(),
            };
        }

        //Костыль
        public static IEnumerable<IMarkupTagsParser> GetAllAvailableParsers()
        {
            var rules = GetAllAvailableRules().ToList();
            return new List<IMarkupTagsParser>()
            {
                new PairedMarkupTagParser(rules),
                new ParagraphTagsParser(rules),
                new SingleMarkupTagsParser(rules),
                new ImageTagParser(rules),
                new LinkTagsParser(rules),
            };
        }

        public static string EscapeSpecialSymbols(string line, int index)
        {
            var symbols = new Dictionary<string, string>()
            {
                {"<", "&lt;"},
                {">", "&gt;"}
				
            };

            var result = new StringBuilder(line);
            var old = result[index];
            result = result.Remove(index, 1);
            result = result.Insert(index, symbols[old.ToString()]);
            return result.ToString();
        }

    }
}
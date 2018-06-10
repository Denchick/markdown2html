using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Markdown.MarkupRules;

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

        public static List<IMarkupRule> GetAllAvailableRules()
        {
            return new List<IMarkupRule>() { new Bold(), new Cursive(), new Header(), new Paragraph(), new Code() };
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
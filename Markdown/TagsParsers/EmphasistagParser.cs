using System.Collections.Generic;
using System.Linq;

namespace Markdown.TagsParsers
{
    public class EmphasisTagParser : IInLineParser
    {
        private List<IMarkupRule> CurrentMarkupRules { get; }

        public EmphasisTagParser(List<IMarkupRule> currentMarkupRules)
        {
            CurrentMarkupRules = currentMarkupRules
                .Where(e => e.HaveClosingMarkupTag)
                .OrderByDescending(e => e.MarkdownTag.Length)
                .ToList();
        }

        public IEnumerable<Token> ParseLine(string line)
        {
            var result = new List<Token>();
            var stackOfOpenedTags = new Stack<Token>();
            for (var index = 0; index < line.Length; index++)
            {
                var rule = DetermineRuleOfSubline(line, index);
                if (rule == null) continue;

                if (Utils.CanBeOpenningTag(line, index))
                    stackOfOpenedTags.Push(new Token(index, rule));
                else if (Utils.CanBeClosingTag(line, index, rule.MarkdownTag.Length))
                {
                    var element = GetClosingElement(stackOfOpenedTags, rule);
                    if (element == null) continue;
                    element.RightBorderOfSubline = index;
                    result.Add(element);
                }
                index += rule.MarkdownTag.Length - 1;
            }
            return result;
        }

        public IEnumerable<Token> ParseMultilineText(string multilineText)
        {
            return  new List<Token>();
        }

        public bool UseParserForBlockText { get; } = false;

        private static Token GetClosingElement(Stack<Token> stack, IMarkupRule rule)
        {
            while (stack.Count > 0)
            {
                var element = stack.Pop();
                if (element.MarkupRule == rule)
                    return element;
            }
            return null;
        }

        private IMarkupRule DetermineRuleOfSubline(string line, int i)
        {

            return CurrentMarkupRules
                .Where(rule => i + rule.MarkdownTag.Length <= line.Length)
                .FirstOrDefault(rule => line.Substring(i, rule.MarkdownTag.Length) == rule.MarkdownTag);
        }
    }
}
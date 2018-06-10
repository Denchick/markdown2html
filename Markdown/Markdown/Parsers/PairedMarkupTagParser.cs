using System.Collections.Generic;
using System.Linq;
using Markdown.MarkupRules;

namespace Markdown.Parsers
{
    public class PairedMarkupTagParser : IMarkupTagsParser
    {
        private List<IMarkupRule> CurrentMarkupRules { get; }

        public PairedMarkupTagParser(List<IMarkupRule> currentMarkupRules)
        {
            CurrentMarkupRules = currentMarkupRules
                .Where(e => e.HaveClosingMarkupTag)
                .OrderByDescending(e => e.MarkupTag.Length)
                .ToList();
        }

        public IEnumerable<ParsedSubline> ParseLine(string line)
        {
            var result = new List<ParsedSubline>();
            var stackOfOpenedTags = new Stack<ParsedSubline>();
            for (var index = 0; index < line.Length; index++)
            {
                var rule = DetermineRuleOfSubline(line, index);
                if (rule == null) continue;

                if (Utils.CanBeOpenningTag(line, index))
                    stackOfOpenedTags.Push(new ParsedSubline(index, rule));
                else if (Utils.CanBeClosingTag(line, index, rule.MarkupTag.Length))
                {
                    var element = GetClosingElement(stackOfOpenedTags, rule);
                    if (element == null) continue;
                    element.RightBorderOfSubline = index;
                    result.Add(element);
                }
                index += rule.MarkupTag.Length - 1;
            }
            return result;
        }

        private static ParsedSubline GetClosingElement(Stack<ParsedSubline> stack, IMarkupRule rule)
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
                .Where(rule => i + rule.MarkupTag.Length <= line.Length)
                .FirstOrDefault(rule => line.Substring(i, rule.MarkupTag.Length) == rule.MarkupTag);
        }
    }
}
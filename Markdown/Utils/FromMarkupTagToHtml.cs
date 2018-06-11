namespace Markdown
{
    public class FromMarkupTagToHtml
    {
        public string TagName { get; }
        public bool IsClosingHtmlTag { get; }
        public int LenghtOfReplacedMarkupTag { get; }
        public IMarkupRule Rule;

        public FromMarkupTagToHtml(string markupRuleHtmlTag, bool isClosingHtmlTag, int lenghtOfReplacedMarkupTag, IMarkupRule rule )
        {
            TagName = markupRuleHtmlTag;
            IsClosingHtmlTag = isClosingHtmlTag;
            LenghtOfReplacedMarkupTag = lenghtOfReplacedMarkupTag;
            Rule = rule;
        }

    }
}
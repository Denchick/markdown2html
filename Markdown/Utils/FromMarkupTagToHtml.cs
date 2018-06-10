namespace Markdown
{
    public class FromMarkupTagToHtml
    {
        public string TagName { get; }
        public bool IsClosingHtmlTag { get; }
        public int LenghtOfReplacedMarkupTag { get; }

        public FromMarkupTagToHtml(string markupRuleHtmlTag, bool isClosingHtmlTag, int lenghtOfReplacedMarkupTag)
        {
            TagName = markupRuleHtmlTag;
            IsClosingHtmlTag = isClosingHtmlTag;
            LenghtOfReplacedMarkupTag = lenghtOfReplacedMarkupTag;
        }

    }
}
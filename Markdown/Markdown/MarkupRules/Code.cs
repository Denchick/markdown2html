namespace Markdown.MarkupRules
{
    public class Code : IMarkupRule
    {
        public string MarkupTag { get; } = "`";
        public string HtmlTag { get; } = "code";
        public bool HaveClosingMarkupTag { get; } = true;
        public bool HaveClosingHtmlTag { get; } = true;
    }
}
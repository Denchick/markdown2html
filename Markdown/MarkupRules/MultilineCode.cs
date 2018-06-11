namespace Markdown.MarkupRules
{
    public class MultilineCode : IMarkupRule
    {
        public string MarkupTag { get; } = "```";
        public string HtmlTag { get; } = "code";
        public bool HaveClosingMarkupTag { get; } = true;
        public bool HaveClosingHtmlTag { get; } = true;
        public bool HasAttribute { get; } = true;
    }
}
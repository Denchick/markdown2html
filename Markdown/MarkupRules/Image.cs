namespace Markdown.MarkupRules
{
    internal class ImageTag : IMarkupRule
    {
        public string MarkupTag { get; set; } = "![";
        public string HtmlTag { get; set; } = "img";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = false;

        //public Image(string MarkdownTag, string link, string text)
        //{
        //    MarkupTag = MarkdownTag;
        //    HtmlTag = "img src=\"" + link + "\"";
        //}
    }
}

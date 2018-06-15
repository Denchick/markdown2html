using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    internal class Headers : IMarkupRule
    {
        public string MarkdownTag { get; } = "#";
        public string HtmlTag { get; } = "h1";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = true;
        public bool HasAttribute { get; } = false;
        public bool UseForBlockText { get; set; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; }
    }

    internal class Header2 : IMarkupRule
    {
        public string MarkdownTag { get; } = "##";
        public string HtmlTag { get; } = "h2";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = true;
        public bool HasAttribute { get; } = false;
        public bool UseForBlockText { get; set; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; }
    }

    internal class Header3 : IMarkupRule
    {
        public Header3()
        {
        }

        public string MarkdownTag { get; } = "###";
        public string HtmlTag { get; } = "h3";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = true;
        public bool HasAttribute { get; } = false;
        public bool UseForBlockText { get; set; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; }
    }

    internal class Header4 : IMarkupRule
    {
        public string MarkdownTag { get; } = "####";
        public string HtmlTag { get; } = "h4";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = true;
        public bool HasAttribute { get; } = false;
        public bool UseForBlockText { get; set; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; }
    }

    internal class Header5 : IMarkupRule
    {
        public string MarkdownTag { get; } = "#####";
        public string HtmlTag { get; } = "h5";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = true;
        public bool HasAttribute { get; } = false;
        public bool UseForBlockText { get; set; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; }
    }

    internal class Header6 : IMarkupRule
    {
        public string MarkdownTag { get; } = "######";
        public string HtmlTag { get; } = "h6";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = true;
        public bool HasAttribute { get; } = false;
        public bool UseForBlockText { get; set; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; }
    }


}

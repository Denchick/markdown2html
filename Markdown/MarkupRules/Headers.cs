using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    internal class Headers : IMarkupRule
    {
        public string MarkupTag { get; } = "#";
        public string HtmlTag { get; } = "h1";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = true;
        public bool HasAttribute { get; } = false;
    }

    internal class Header2 : IMarkupRule
    {
        public string MarkupTag { get; } = "##";
        public string HtmlTag { get; } = "h2";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = true;
        public bool HasAttribute { get; } = false;
    }

    internal class Header3 : IMarkupRule
    {
        public string MarkupTag { get; } = "###";
        public string HtmlTag { get; } = "h3";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = true;
        public bool HasAttribute { get; } = false;
    }

    internal class Header4 : IMarkupRule
    {
        public string MarkupTag { get; } = "####";
        public string HtmlTag { get; } = "h4";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = true;
        public bool HasAttribute { get; } = false;
    }

    internal class Header5 : IMarkupRule
    {
        public string MarkupTag { get; } = "#####";
        public string HtmlTag { get; } = "h5";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = true;
        public bool HasAttribute { get; } = false;
    }

    internal class Header6 : IMarkupRule
    {
        public string MarkupTag { get; } = "######";
        public string HtmlTag { get; } = "h6";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingHtmlTag { get; } = true;
        public bool HasAttribute { get; } = false;
    }


}

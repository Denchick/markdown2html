using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    internal class Headers : IMarkupRule
    {
        public string MarkdownTag { get; } = "#";
        public string Tag { get; } = "h1";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingTag { get; } = true;
        public bool HasAttribute { get; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; } = "";
        public bool UseForMultiline { get; } = false;
    }

    internal class Header2 : IMarkupRule
    {
        public string MarkdownTag { get; } = "##";
        public string Tag { get; } = "h2";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingTag { get; } = true;
        public bool HasAttribute { get; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; } = "";
        public bool UseForMultiline { get; } = false;
    }

    internal class Header3 : IMarkupRule
    {

        public string MarkdownTag { get; } = "###";
        public string Tag { get; } = "h3";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingTag { get; } = true;
        public bool HasAttribute { get; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; } = "";
        public bool UseForMultiline { get; } = false;
    }

    internal class Header4 : IMarkupRule
    {
        public string MarkdownTag { get; } = "####";
        public string Tag { get; } = "h4";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingTag { get; } = true;
        public bool HasAttribute { get; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; } = "";
        public bool UseForMultiline { get; } = false;
    }

    internal class Header5 : IMarkupRule
    {
        public string MarkdownTag { get; } = "#####";
        public string Tag { get; } = "h5";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingTag { get; } = true;
        public bool HasAttribute { get; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; } = "";
        public bool UseForMultiline { get; } = false;
    }

    internal class Header6 : IMarkupRule
    {
        public string MarkdownTag { get; } = "######";
        public string Tag { get; } = "h6";
        public bool HaveClosingMarkupTag { get; } = false;
        public bool HaveClosingTag { get; } = true;
        public bool HasAttribute { get; } = false;
        public IEnumerable<TagAttribute> Attributes { get; }
        public string TextInsideTag { get; } = "";
        public bool UseForMultiline { get; } = false;
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown
{
    public interface IMarkupRule
    {
        string MarkupTag { get; }
        string HtmlTag { get; }
        bool HaveClosingMarkupTag { get; }
        bool HaveClosingHtmlTag { get; }
        bool HasAttribute { get; }
    }
}

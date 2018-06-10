using System.Collections.Generic;
using FluentAssertions;
using Markdown.MarkupRules;
using NUnit.Framework;

namespace Markdown
{
    [TestFixture]
    public class Md_ShouldRender
    {

        [TestCase("")]
        [TestCase("kek")]
        [TestCase("this text is real complex")]
        [TestCase("kek_cheburek")]
        [TestCase("_kek")]
        [TestCase("kek_")]
        [TestCase("ke___k")]
        [TestCase("k#ek")]
        [TestCase("kek#")]
        public void CorrectMarkup_WhenNothingToMarkUp(string s)
        {
            var rules = Utils.GetAllAvailableRules();
            
            var md = new Md(rules);

            md.RenderToHtml(s).Contains($"<p>{s}</p>");
        }
    }
}
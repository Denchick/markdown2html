using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Markdown.MarkupRules;
using NUnit.Framework;

namespace Markdown
{
    [TestFixture]
    public class TextRender_Should
    {
        [TestCase("_kek_", "_", 0, 4, "<p><em>kek</em></p>")]
        [TestCase("__kek__", "__", 0, 5, "<p><strong>kek</strong></p>")]
        [TestCase("___kek__", "__", 0, 6, "<p><strong>_kek</strong></p>")]
        [TestCase("__kek___", "__", 0, 5, "<p><strong>kek</strong>_</p>")]
        public void CorrectRending_WhenNeedsRenderingOneTag(string line, string markupTag, 
            int leftBorderOfSubline, int rightBorderOfSubline, string expected)
        {   
            var render = new TextRender(Utils.GetAllAvailableRules());
            var parsed = new List<ParsedSubline>()
            {
                new ParsedSubline(leftBorderOfSubline, rightBorderOfSubline, 
                    Utils.GetAllAvailableRules().First(e => e.MarkupTag == markupTag)),
                new ParsedSubline(-1, line.Length, new Paragraph())
            };
            
            var actual = render.RenderLine(line, parsed);
            
            actual.Should().Be(expected);
        }
        
        [Test]
        public void CorrectRendering_WhenFewTagsInLine()
        {
            var line = "_a_ __b__";
            var cursiveTag = new ParsedSubline(0, 2, new Cursive());
            var boldTag = new ParsedSubline(4, 7, new Bold());
            var paragraphTag = new ParsedSubline(-1, line.Length, new Paragraph());
            var parsed = new List<ParsedSubline>() { cursiveTag, boldTag, paragraphTag };
            
            var render = new TextRender(Utils.GetAllAvailableRules());
            var result = render.RenderLine(line, parsed);

            result.Should().BeEquivalentTo("<p><em>a</em> <strong>b</strong></p>");
        }

        [Test]
        public void CorrectRendering_WhenNestingTagsInLine()
        {
            var line = "#_a_";
            var headerTag = new ParsedSubline(0, line.Length, new Header());
            var boldTag = new ParsedSubline(1, 3, new Cursive());
            var parsed = new List<ParsedSubline>() { headerTag, boldTag };
            
            var render = new TextRender(Utils.GetAllAvailableRules());
            var result = render.RenderLine(line, parsed);

            result.Should().BeEquivalentTo("<h1><em>a</em></h1>");        
        }

        [Test]
        public void CorrectRendering_WhenRenderHeaderTag()
        {
            var line = "#kek";
            var render = new TextRender(Utils.GetAllAvailableRules());
            var parsed = new List<ParsedSubline>()
            {
                new ParsedSubline(0, 4, Utils.GetAllAvailableRules().First(e => e.MarkupTag == "#"))
            };
            
            var actual = render.RenderLine(line, parsed);
            
            actual.Should().Be("<h1>kek</h1>");

        }
        
    }
}
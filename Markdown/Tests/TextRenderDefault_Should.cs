﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Markdown.MarkupRules;
using Markdown.Parsers;
using Markdown.TagsParsers;
using NUnit.Framework;

namespace Markdown
{
    [TestFixture]
    public class TextRenderDefault_Should
    {
        [TestCase("_kek_", "_", 0, 4, "<p><em>kek</em></p>")]
        [TestCase("__kek__", "__", 0, 5, "<p><strong>kek</strong></p>")]
        [TestCase("___kek__", "__", 0, 6, "<p><strong>_kek</strong></p>")]
        [TestCase("__kek___", "__", 0, 5, "<p><strong>kek</strong>_</p>")]
        public void CorrectRending_WhenNeedsRenderingOneTag(string line, string markupTag, 
            int leftBorderOfSubline, int rightBorderOfSubline, string expected)
        {   
            var render = new DefaultHtmlConverter(Utils.GetAllAvailableRules(), Utils.GetAllAvailableParsers());
            var parsed = new List<Token>()
            {
                new Token(leftBorderOfSubline, rightBorderOfSubline, 
                    Utils.GetAllAvailableRules().First(e => e.MarkdownTag == markupTag)),
                new Token(-1, line.Length, new Paragraph())
            };
            
            var actual = render.RenderLine(line, parsed);
            
            actual.Should().Be(expected);
        }
        
        [Test]
        public void CorrectRendering_WhenFewTagsInLine()
        {
            var line = "_a_ __b__";
            var cursiveTag = new Token(0, 2, new CursiveRuleWithSingleUnderscores());
            var boldTag = new Token(4, 7, new BoldRuleWithDoubleUnderscores());
            var paragraphTag = new Token(-1, line.Length, new Paragraph());
            var parsed = new List<Token>() { cursiveTag, boldTag, paragraphTag };
            
            var render = new DefaultHtmlConverter(Utils.GetAllAvailableRules(), Utils.GetAllAvailableParsers());
            var result = render.RenderLine(line, parsed);

            result.Should().BeEquivalentTo("<p><em>a</em> <strong>b</strong></p>");
        }

        [Test]
        public void CorrectRendering_WhenNestingTagsInLine()
        {
            var line = "#_a_";
            var headerTag = new Token(0, line.Length, new Headers());
            var boldTag = new Token(1, 3, new CursiveRuleWithSingleUnderscores());
            var parsed = new List<Token>() { headerTag, boldTag };
            
            var render = new DefaultHtmlConverter(Utils.GetAllAvailableRules(), Utils.GetAllAvailableParsers());
            var result = render.RenderLine(line, parsed);

            result.Should().BeEquivalentTo("<h1><em>a</em></h1>");        
        }

        [Test]
        public void CorrectRendering_WhenRenderHeaderTag()
        {
            var line = "#kek";
            var render = new DefaultHtmlConverter(Utils.GetAllAvailableRules(), Utils.GetAllAvailableParsers());
            var parsed = new List<Token>()
            {
                new Token(0, 4, Utils.GetAllAvailableRules().First(e => e.MarkdownTag == "#"))
            };
            
            var actual = render.RenderLine(line, parsed);
            
            actual.Should().Be("<h1>kek</h1>");

        }

        [TestCase("")]
        [TestCase("kek")]
        [TestCase("this text is real complex")]
        [TestCase("kek_cheburek")]
        [TestCase("_kek")]
        [TestCase("kek_")]
        [TestCase("ke___k")]
        [TestCase("k#ek")]
        [TestCase("kek#")]
        public void CorrectMarkup_WhenNothingToMarkup(string s)
        {
            var render = new DefaultHtmlConverter(Utils.GetAllAvailableRules(), Utils.GetAllAvailableParsers());

            render.ConvertToFormat(s).Contains($"<p>{s}</p>");
        }

        [Test]
        public void CorrectMarkup_WhenMultipleTags_WithOneLineBreak()
        {
            var input = @"# Заголовок первого уровня
## Заголовок h2
### Заголовок h3
#### Заголовок h4
##### Заголовок h5
###### Заголовок h6";
            var expected = @"<h1>Заголовок первого уровня</h1>
<h2>Заголовок h2</h2>
<h3>Заголовок h3</h3>
<h4>Заголовок h4</h4>
<h5>Заголовок h5</h5>
<h6>Заголовок h6</h6>";
            var render = new DefaultHtmlConverter(Utils.GetAllAvailableRules(), Utils.GetAllAvailableParsers());

            render.ConvertToFormat(input).Contains(expected);
        }

        [Test]
        public void CorrectMarkup_WhenHeaderTagHaveClosingMarkupTag()
        {
            var input = @"# Заголовок #";
            var render = new DefaultHtmlConverter(Utils.GetAllAvailableRules(), Utils.GetAllAvailableParsers());

            render.ConvertToFormat(input).Equals("<h1>Заголовок</h1>");
        }

        [TestCase("***")]
        [TestCase("* * *")]
        [TestCase("---")]
        [TestCase("- - -")]
        [TestCase("*** ")]
        [TestCase(" ***")]
        [TestCase("*** keke")]
        [TestCase("ewf ***")]
        [TestCase("kek***")]
        public void CorrectMarkup_WhenHorizontalRule(string s)
        {
            var render = new DefaultHtmlConverter(Utils.GetAllAvailableRules(), Utils.GetAllAvailableParsers());

            render.ConvertToFormat(s).Contains($"<hr>");
        }

        [TestCase("![авыаыв	](http://p/1.jpg)", "http://p/1.jpg", "авыаыв	")]
        [TestCase("![](http://p/1.jpg)", "http://p/1.jpg", "")]
        [TestCase("![1233456]()", "", "1233456")]
        [TestCase("hello my dear friend! ![file not found](http://p/1.jpg)", "http://p/1.jpg", "file not found", "hello my dear friend! ")]
        public void CorrectRenderImage(string text, string link, string alt, string otherText = "")
        {
            var imgTag = new IMarkupRule[] {new ImageTag(),};
            var render = new DefaultHtmlConverter(imgTag, new IParser[] {new ImageTagParser(imgTag)});
            var html = $"{otherText}<img src=\"{link}\" alt=\"{alt}\">\r\n";
            var result = render.ConvertToFormat(text);
            result.Should().BeEquivalentTo(html);
        }
        
        [TestCase("> kek kek kek")]
        public void CorrectConvertInlineQuotation(string quotation)
        {
            var expected =  $"<blockquote><p>{quotation.TrimStart('>')}</p></blockquote>\r\n";
            var converter = new DefaultHtmlConverter(Utils.GetAllAvailableRules(), Utils.GetAllAvailableParsers());

            var result = converter.ConvertToFormat(quotation);
            result.Should().BeEquivalentTo(expected);
        }

        
        [TestCase("> kek \r\n>kek\r\n>kek")]
        public void CorrectConvertMultilineQuotation(string quotation)
        {
            var lines = quotation.Split(new[] { "\r\n" }, StringSplitOptions.None).Select(line => line.TrimStart('>')).ToArray();
            for (int i = 0; i < lines.Count(); i++)
            {   
                lines[i] = $"<p>{lines[i]}</p>";
            }
            var expected = $"<blockquote>{string.Join("\r\n", lines)}</blockquote>\r\n";
            var converter = new DefaultHtmlConverter(Utils.GetAllAvailableRules(), Utils.GetAllAvailableParsers());
            var result = converter.ConvertToFormat(quotation);
            result.Should().BeEquivalentTo(expected);
        }
        [TestCase(">kek\r\n>>kek\r\n>kek", "<blockquote><p>kek</p>\r\n<blockquote><p>kek</p></blockquote>\r\n<p>kek</p></blockquote>\r\n")]
        public void CorrectConvertNesting(string quotation, string expected)
        {
            var converter = new DefaultHtmlConverter(Utils.GetAllAvailableRules(), Utils.GetAllAvailableParsers());
            var result = converter.ConvertToFormat(quotation);
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("> *Header in quote* ", "<blockquote><p> <em>Header in quote</em> </p></blockquote>\r\n")]
        public void ConvertTextWithNesting(string text, string expected)
        {
            var converter = new DefaultHtmlConverter(Utils.GetAllAvailableRules(), Utils.GetAllAvailableParsers());
            var result = converter.ConvertToFormat(text);
            result.Should().BeEquivalentTo(expected);
        }
        
    }
    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using Markdown.MarkupRules;
using Markdown.Parsers;
using Markdown.TagsParsers;
using NUnit.Framework;

namespace Markdown
{
    [TestFixture]
    public class TextParser_Should
    {
        [TestCase("", TestName = "when line is empty")]
        [TestCase(" ", TestName = "when line is whitespace")]
        [TestCase("kek", TestName = "when line is just one word")]
        [TestCase(" kek", TestName = "when whitespace before word")]
        [TestCase("_kek", TestName = "when closing tag does not have a pair")]
        [TestCase("kek_", TestName = "when closing tag does not have a pair")]
        [TestCase("kek ", TestName = "when whitespace after word")]
        [TestCase("just simple text", TestName = "when line is some words separated by spacies")]
        public void CorrectParsing_WhenNothingToParse(string line)
        {
            var rules = Utils.GetAllAvailableRules();
            
            var parser = new TextParser(rules, Utils.GetAllAvailableParsers());
            var result = parser.ParseLine(line);

            var expected = new List<Token>()
            {
                new Token(-1, line.Length, new Paragraph())
            };
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("_kek_", "_", 0, 4)]
        [TestCase("__kek__", "__", 0, 5)]
        [TestCase("___kek__", "__", 0, 6)]
        [TestCase("__kek___", "__", 0, 5)]
        [TestCase("_a\nb_", "_", 0, 4)]
        public void CorrectParsing_WhenOnePairedMarkupTagInLine(string line, string markupTag, int leftParsedIndex, int rightParsedIndex)
        {
            var parser = new TextParser(Utils.GetAllAvailableRules(), Utils.GetAllAvailableParsers());
            var result = parser.ParseLine(line);

            var expected = new List<Token>()
            {
                new Token(-1, line.Length, Utils.GetAllAvailableRules()
                    .First(e => e.HtmlTag == "p")),
                new Token(leftParsedIndex, rightParsedIndex, Utils.GetAllAvailableRules()
                    .First(e => e.MarkupTag == markupTag))
            };
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("#kek", "#", 0, 4)]
        [TestCase("# kek", "#", 0, 5)]
        [TestCase("## kek", "##", 0, 6)]
        [TestCase("### kek", "###", 0, 7)]
        [TestCase("###### kek", "######", 0, 10)]
        public void CorrectParsing_WhenOneSingleMarkupTagInLine(string line, string markupTag, int leftParsedIndex, int rightParsedIndex)
        {
            var parser = new TextParser(Utils.GetAllAvailableRules(), Utils.GetAllAvailableParsers());
            var result = parser.ParseLine(line);

            var expected = new List<Token>()
            {
                new Token(leftParsedIndex, rightParsedIndex, Utils.GetAllAvailableRules()
                    .First(e => e.MarkupTag == markupTag))
            };
            result.Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public void CorrectParsing_WhenFewTagsInLine()
        {
            var line = "_a_ __b__";
            
            var parser = new TextParser(Utils.GetAllAvailableRules(), Utils.GetAllAvailableParsers());
            var result = parser.ParseLine(line);

            var cursiveTag = new Token(0, 2, new Cursive());
            var boldTag = new Token(4, 7, new Bold());
            var paragraphTag = new Token(-1, line.Length, new Paragraph());
            var expected = new List<Token>() { cursiveTag, boldTag, paragraphTag };
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void CorrectParsing_WhenNestingTagsInLine()
        {
            var line = "#_a_";
            
            var parser = new TextParser(Utils.GetAllAvailableRules(), Utils.GetAllAvailableParsers());
            var result = parser.ParseLine(line);

            var headerTag = new Token(0, line.Length, new Headers());
            var boldTag = new Token(1, 3, new Cursive());
            var expected = new List<Token>() { headerTag, boldTag };
            result.Should().BeEquivalentTo(expected);
        }

        private readonly Regex getMarkdowFromText = new Regex("!\\[.*\\]\\(.*\\)");
        private List<IMarkupRule> imgTag = new List<IMarkupRule>() { new ImageTag() };

        [TestCase("![авыаыв	](http://p/1.jpg)", "http://p/1.jpg", "авыаыв	", 0)]
        [TestCase("![](123longNameWhyNotIthinks.png)", "123longNameWhyNotIthinks.png", "", 0)]
        [TestCase("![3123321]()", "", "3123321", 0)]
        [TestCase("!![авыаывsdfsdfdsfsdf](http://p/1.jpg)", "http://p/1.jpg", "авыаывsdfsdfdsfsdf", 1)]
        [TestCase("asasas ![авыаыв	](http://p/1.jpg)", "http://p/1.jpg", "авыаыв	", 7)]
        public void CorrectParsingImageInSimpleLine(string text, string link, string alt, int leftBound)
        {
            var parser = new TextParser(imgTag, new List<IParser>() { new ImageTagParser(imgTag) });
            var result = parser.ParseLine(text);
            var attributes = new List<TagAttribute>(){new TagAttribute(link, "src"), new TagAttribute(alt, "alt")};
            var imageTag = new ImageTag() {HtmlTag = "img", MarkupTag = getMarkdowFromText.Match(text).Value, Attributes = attributes};
            var expected = new List<Token>(){new Token(leftBound, imageTag)};

            result.Should().BeEquivalentTo(expected);
        }
        
        [TestCase(" ![]http://p/1.jpg)")]
        [TestCase("[авыаыв	]!(http://p/1.jpg)")]
        [TestCase("![dasbdjlsabjfbsbfbashbasbhfbsabfbshafhjsbfhbsahjfbhjsabfjaflbhal]")]
        public void ParsingIncorrectImageTag(string text)
        {
            var parser = new TextParser(imgTag, new List<IParser>(){new ImageTagParser(imgTag) } );
            var result = parser.ParseLine(text);
            var expected = new List<Token>() { };
            result.Should().BeEquivalentTo(expected);
        }
    }
}
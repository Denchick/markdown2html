using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Markdown.MarkupRules;
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
            
            var parser = new TextParser(rules);
            var result = parser.ParseLine(line);

            var expected = new List<ParsedSubline>()
            {
                new ParsedSubline(-1, line.Length, new Paragraph())
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
            var parser = new TextParser(Utils.GetAllAvailableRules());
            var result = parser.ParseLine(line);

            var expected = new List<ParsedSubline>()
            {
                new ParsedSubline(-1, line.Length, Utils.GetAllAvailableRules()
                    .First(e => e.HtmlTag == "p")),
                new ParsedSubline(leftParsedIndex, rightParsedIndex, Utils.GetAllAvailableRules()
                    .First(e => e.MarkupTag == markupTag))
            };
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("#kek", "#", 0, 4)]
        [TestCase("# kek", "#", 0, 5)]
        public void CorrectParsing_WhenOneSingleMarkupTagInLine(string line, string markupTag, int leftParsedIndex, int rightParsedIndex)
        {
            var parser = new TextParser(Utils.GetAllAvailableRules());
            var result = parser.ParseLine(line);

            var expected = new List<ParsedSubline>()
            {
                new ParsedSubline(leftParsedIndex, rightParsedIndex, Utils.GetAllAvailableRules()
                    .First(e => e.MarkupTag == markupTag))
            };
            result.Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public void CorrectParsing_WhenFewTagsInLine()
        {
            var line = "_a_ __b__";
            
            var parser = new TextParser(Utils.GetAllAvailableRules());
            var result = parser.ParseLine(line);

            var cursiveTag = new ParsedSubline(0, 2, new Cursive());
            var boldTag = new ParsedSubline(4, 7, new Bold());
            var paragraphTag = new ParsedSubline(-1, line.Length, new Paragraph());
            var expected = new List<ParsedSubline>() { cursiveTag, boldTag, paragraphTag };
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void CorrectParsing_WhenNestingTagsInLine()
        {
            var line = "#_a_";
            
            var parser = new TextParser(Utils.GetAllAvailableRules());
            var result = parser.ParseLine(line);

            var headerTag = new ParsedSubline(0, line.Length, new Header());
            var boldTag = new ParsedSubline(1, 3, new Cursive());
            var expected = new List<ParsedSubline>() { headerTag, boldTag };
            result.Should().BeEquivalentTo(expected);
        }
    }
}
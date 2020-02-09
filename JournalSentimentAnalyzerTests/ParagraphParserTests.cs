using System;
using System.Collections.Generic;
using JournalSentimentAnalyzer;
using Xunit;

namespace JournalSentimentAnalyzerTests
{
    public class ParagraphParserTests
    {
        [Fact]
        public void ParagraphParser_EmptyCase()
        {
            List<string> input = new List<string>() { "", " ", "" };

            List<Paragraph> paragraphs = ParagraphParser.ParseParagraphs(input.ToArray());
            Assert.Empty(paragraphs);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ParagraphParser_BasicTest(bool trailingLines)
        {
            List<string> input = new List<string>();

            input.Add("This is the first sentence of the first paragraph.");
            input.Add("Each sentence is on one line.");
            input.Add("There is a blank line between paragraphs.");
            input.Add("");
            input.Add("Some paragraphs will be longer than others.");
            input.Add("Paragraphs with more sentences will receive higher weight scores.");
            input.Add("");
            input.Add("There could be trailing lines at the end of the file, but they should be ignored.");

            if (trailingLines)
            {
                input.Add("");
                input.Add("");
            }

            List<Paragraph> paragraphs = ParagraphParser.ParseParagraphs(input.ToArray());

            Assert.Equal(3, paragraphs.Count);
            Assert.Equal(3, paragraphs[0].Weight);
            Assert.Equal(2, paragraphs[1].Weight);
            Assert.Equal(1, paragraphs[2].Weight);

            Assert.Equal(
                "This is the first sentence of the first paragraph."+
                "Each sentence is on one line." +
                "There is a blank line between paragraphs.",
                paragraphs[0].ParagraphText);
            Assert.Equal(
                "Some paragraphs will be longer than others." +
                "Paragraphs with more sentences will receive higher weight scores.",
                paragraphs[1].ParagraphText);
            Assert.Equal(
                "There could be trailing lines at the end of the file, but they should be ignored.",
                paragraphs[2].ParagraphText);
        }
    }
}

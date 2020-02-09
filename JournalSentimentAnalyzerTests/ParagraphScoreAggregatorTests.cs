using JournalSentimentAnalyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JournalSentimentAnalyzerTests
{
    public class ParagraphScoreAggregatorTests
    {
        [Fact]
        public void ParagraphScoreAggregator_EmptyCase()
        {
            Assert.Equal(double.NaN, ParagraphScoreAggregator.GetAggregateWeightedScore(new List<Paragraph>()));
        }

        [Fact]
        public void ParagraphScoreAggregator_ComplexCase()
        {
            var paragraphs = new List<Paragraph>();
            paragraphs.Add(new Paragraph("AAA", 1));
            paragraphs.Add(new Paragraph("BBB", 1));
            paragraphs.Add(new Paragraph("CCC", 2));
            paragraphs.Add(new Paragraph("DDD", 4));

            paragraphs[0].SentimentScore = 0.25;
            paragraphs[1].SentimentScore = 0.5;
            paragraphs[2].SentimentScore = 0.75;
            paragraphs[3].SentimentScore = 1;

            Assert.Equal(0.78125, ParagraphScoreAggregator.GetAggregateWeightedScore(paragraphs));
        }
    }
}

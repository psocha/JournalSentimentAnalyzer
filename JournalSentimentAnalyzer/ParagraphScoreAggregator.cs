using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSentimentAnalyzer
{
    public static class ParagraphScoreAggregator
    {
        public static double GetAggregateWeightedScore(List<Paragraph> paragraphs)
        {
            int totalWeighting = paragraphs.Sum(p => p.Weight);

            if (totalWeighting == 0)
            {
                return double.NaN;
            }

            double weightedSum = 0;
            foreach (Paragraph paragraph in paragraphs)
            {
                double paragraphScore = paragraph.SentimentScore * paragraph.Weight / totalWeighting;
                weightedSum += paragraphScore;
            }

            return weightedSum;
        }
    }
}

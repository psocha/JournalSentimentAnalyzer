using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSentimentAnalyzer
{
    public class Paragraph
    {
        public string ParagraphText { get; set; }
        public int Weight { get; set; }
        public double SentimentScore { get; set; }

        public Paragraph(string paragraphText, int weight)
        {
            this.ParagraphText = paragraphText;
            this.Weight = weight;
        }
    }
}

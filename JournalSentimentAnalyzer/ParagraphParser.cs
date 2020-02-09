using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSentimentAnalyzer
{
    public class ParagraphParser
    {
        public static List<Paragraph> ParseParagraphs(string[] sentences)
        {
            List<Paragraph> paragraphs = new List<Paragraph>();

            string currentParagraph = "";
            int currentParagraphLength = 0;

            foreach (string sentence in sentences)
            {
                if (string.IsNullOrWhiteSpace(sentence))
                {
                    if (currentParagraph.Length > 0 && !string.IsNullOrWhiteSpace(currentParagraph))
                    {
                        paragraphs.Add(new Paragraph(currentParagraph, currentParagraphLength));
                        currentParagraph = "";
                        currentParagraphLength = 0;
                    }
                }
                else
                {
                    currentParagraph += sentence;
                    currentParagraphLength++;
                }
            }

            if (currentParagraphLength > 0 && !string.IsNullOrWhiteSpace(currentParagraph))
            {
                paragraphs.Add(new Paragraph(currentParagraph, currentParagraphLength));
            }

            return paragraphs;
        }
    }
}

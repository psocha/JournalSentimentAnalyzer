using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSentimentAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime startDate = new DateTime(2019, 1, 3);
            DateTime endDate = new DateTime(2019, 12, 31);
            DateTime currentDate = startDate;

            while (currentDate <= endDate)
            {
                string formattedDate = currentDate.ToString("yyyy-MM-dd");
                string fileName = formattedDate + ".txt";

                try
                {
                    string[] sentences = File.ReadAllLines(fileName);
                    List<Paragraph> paragraphs = ParagraphParser.ParseParagraphs(sentences);

                    Console.WriteLine($"{fileName} has {paragraphs.Count} paragraphs.");

                    currentDate = currentDate.AddDays(1);
                }
                catch (FileNotFoundException)
                {
                    Console.Error.WriteLine($"{fileName} not found. Skipping...");
                }
            }

        }
    }
}

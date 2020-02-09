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

            var sentimentClient = new SentimentClient();

            Dictionary<string, List<double>> perParagraphScores = new Dictionary<string, List<double>>();
            Dictionary<string, double> globalScores = new Dictionary<string, double>();

            while (currentDate <= endDate)
            {
                string formattedDate = currentDate.ToString("yyyy-MM-dd");
                string fileName = formattedDate + ".txt";

                try
                {
                    string[] sentences = File.ReadAllLines(fileName);
                    List<Paragraph> paragraphs = ParagraphParser.ParseParagraphs(sentences);

                    Console.WriteLine($"{fileName} has {paragraphs.Count} paragraphs.");

                    for (int i = 0; i < paragraphs.Count; i++)
                    {
                        Paragraph paragraph = paragraphs[i];

                        paragraph.SentimentScore = sentimentClient.GetSentimentScore(paragraph.ParagraphText);

                        if (!perParagraphScores.ContainsKey(formattedDate))
                        {
                            perParagraphScores[formattedDate] = new List<double>();
                        }

                        perParagraphScores[formattedDate].Add(paragraph.SentimentScore);
                        Console.WriteLine($"{formattedDate} paragraph {i} score: {paragraph.SentimentScore}");
                    }

                    double globalDayScore = ParagraphScoreAggregator.GetAggregateWeightedScore(paragraphs);
                    globalScores[formattedDate] = globalDayScore;
                    Console.WriteLine($"{formattedDate} global day score: {globalDayScore}");

                    currentDate = currentDate.AddDays(1);
                }
                catch (FileNotFoundException)
                {
                    Console.Error.WriteLine($"{fileName} not found. Skipping...");
                }
            }

            StringBuilder paragraphCsv = new StringBuilder();
            foreach (string day in perParagraphScores.Keys)
            {
                paragraphCsv.Append(day);
                foreach (double paragraphScore in perParagraphScores[day])
                {
                    paragraphCsv.Append(",");
                    paragraphCsv.Append(paragraphScore.ToString());
                }
                paragraphCsv.Append("\n");
            }
            string perParagraphFileName = $"PerParagraphSentimentScores_{DateTime.UtcNow.ToString("yyyy-MM-dd-hh-mm-ss")}.csv";
            File.WriteAllText(perParagraphFileName, paragraphCsv.ToString());
            Console.WriteLine($"Output file {perParagraphFileName}");

            StringBuilder dailyCsv = new StringBuilder();
            foreach (string day in globalScores.Keys)
            {
                dailyCsv.AppendLine($"{day},{globalScores[day].ToString()}");
            }
            string perDayFileName = $"PerDaySentimentScores_{DateTime.UtcNow.ToString("yyyy-MM-dd-hh-mm-ss")}.csv";
            File.WriteAllText(perDayFileName, dailyCsv.ToString());
            Console.WriteLine($"Output file {perDayFileName}");
        }
    }
}

using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Rest;

namespace JournalSentimentAnalyzer
{
    public class SentimentClient
    {
        public SentimentClient()
        {
        }

        public double GetSentimentScore(string text)
        {
            return (double)text.Split(' ').Length / (double)100;
        }
    }
}

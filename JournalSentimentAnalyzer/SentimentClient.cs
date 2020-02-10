using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;

namespace JournalSentimentAnalyzer
{
    public class SentimentClient
    {
        private const string key = "YOUR_KEY_HERE";
        private const string endpoint = "YOUR_ENDPOINT_HERE";

        private TextAnalyticsClient client;

        public SentimentClient()
        {
            ApiKeyServiceClientCredentials credentials = new ApiKeyServiceClientCredentials(key);
            this.client = new TextAnalyticsClient(credentials)
            {
                Endpoint = endpoint
            };
        }

        public double GetSentimentScore(string text)
        {
            int retryCount = 0;
            while (retryCount < 3)
            {
                try
                {
                    var sentimentResult = client.Sentiment(text);
                    return sentimentResult.Score ?? 0.5;
                }
                catch (TaskCanceledException)
                {
                    retryCount++;
                    Console.WriteLine($"TaskCanceledException, retry #{retryCount}");
                }
            }

            Console.Error.WriteLine("Retry limit reached, returning neutral score");
            return 0.5;
        }
    }
}

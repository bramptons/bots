using System;
using System.Net;
using Newtonsoft.Json;

namespace apis
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Data Protector Bot");

            string responseString = string.Empty;

            var query = Console.ReadLine();
            var knowledgebaseId = "a5b2946d-2c7e-4783-b140-8428cdae2957";
            var qnamakerSubscriptionKey = "756c437e82cd4da5b3b3d37b5968697f";

            Uri qnamakerUriBase = new Uri("https://westus.api.cognitive.microsoft.com/qnamaker/v1.0");
            var builder = new UriBuilder($"{qnamakerUriBase}/knowledgebases/{knowledgebaseId}/generateAnswer");

            var postBody = $"{{\"question\":\"{query}\"}}";

            using(WebClient client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;

                client.Headers.Add("Ocp-Apim-Subscription-Key",qnamakerSubscriptionKey);
                client.Headers.Add("Content-Type","application/json");
                responseString = client.UploadString(builder.Uri, postBody);
            }

            QnAMakerResult response;
            try
            {
                response = JsonConvert.DeserializeObject<QnAMakerResult>(responseString);
                Console.WriteLine(response.Answer);
                Console.WriteLine(response.Score);
            }
            catch
            {
                throw new Exception("unable to deserilaize QnA service");
            }
        }

    }

    class QnAMakerResult
    {
        [JsonProperty(PropertyName = "answer")]
        public string Answer {get; set;}
        [JsonProperty(PropertyName = "score")]
        public double Score {get; set;}
    }
}

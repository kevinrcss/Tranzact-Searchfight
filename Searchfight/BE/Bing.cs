using Newtonsoft.Json.Linq;
using Searchfight.Interfaces;
using System;
using System.IO;
using System.Net;

namespace Searchfight.BE
{
    public class Bing : ISearchEngine
    {        
        public string Name { get; set; }

        private readonly string AccessKey = Environment.GetEnvironmentVariable("BING_SEARCH_V7_SUBSCRIPTION_KEY");
        private readonly string Endpoint = Environment.GetEnvironmentVariable("BING_SEARCH_V7_ENDPOINT");
        public Bing()
        {
            Name = "Bing";
        }
        public string GetResultsDetail(long results)
        {
            return $"{Name}: {results}";
        }

        public long GetSearchResults(string query)
        {
            long matches = 0;
            try
            {
                string uriQuery = Endpoint + "?q=" + Uri.EscapeDataString(query);

                WebRequest request = HttpWebRequest.Create(uriQuery);
                request.Headers["Ocp-Apim-Subscription-Key"] = AccessKey;
                HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
                string jsonResponse = new StreamReader(response.GetResponseStream()).ReadToEnd();
                JObject objJsonResponse = JObject.Parse(jsonResponse);
                matches = objJsonResponse["webPages"]["totalEstimatedMatches"].ToObject<long>();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            return matches;
        }
    }
}

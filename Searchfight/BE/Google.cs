using Newtonsoft.Json.Linq;
using Searchfight.Interfaces;
using System;
using System.IO;
using System.Net;

namespace Searchfight.BE
{
    public class Google : ISearchEngine
    {
        public string Name { get; set; }

        private readonly string ApiKey = Environment.GetEnvironmentVariable("GOOGLE_SEARCH_API_KEY");
        private readonly string CX = Environment.GetEnvironmentVariable("GOOGLE_SEARCH_ENGINE_ID");
        private readonly string UriBase = Environment.GetEnvironmentVariable("GOOGLE_SEARCH_API_ENDPOINT");
        public Google()
        {
            Name = "Google";
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
                string uriQuery = UriBase + "?key=" + ApiKey + "&cx=" + CX + "&q=" + Uri.EscapeDataString(query);

                WebRequest request = HttpWebRequest.Create(uriQuery);
                HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
                string jsonResponse = new StreamReader(response.GetResponseStream()).ReadToEnd();
                JObject objJsonResponse = JObject.Parse(jsonResponse);
                matches = objJsonResponse["searchInformation"]["totalResults"].ToObject<long>();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return matches;
        }
    }
}

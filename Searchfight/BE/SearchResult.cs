using Searchfight.Interfaces;
using System.Collections.Generic;

namespace Searchfight.BE
{
    public class SearchResult
    {
        public ISearchEngine SearchEngine { get; set; }
        public string Query { get; set; }
        public long Amount { get; set; }
        public SearchResult()
        {

        }
        public SearchResult(ISearchEngine searchEngine, string query, long amount)
        {
            SearchEngine = searchEngine;
            Query = query;
            Amount = amount;
        }

        public virtual List<SearchResult> GetSearchResults(IList<ISearchEngine> searchEngineList, string[] queryArray)
        {
            List<SearchResult> resultList = new List<SearchResult>();
            SearchResult result;

            foreach (var searchEngine in searchEngineList)
            {
                foreach (var query in queryArray)
                {
                    result = new SearchResult(searchEngine, query, searchEngine.GetSearchResults(query));
                    resultList.Add(result);
                }
            }
            return resultList;
        }
    }
}

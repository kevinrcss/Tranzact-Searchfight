using Searchfight.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Searchfight.BE
{
    public class FinalResult
    {
        public string ResultDetails(List<SearchResult> lstSearchResults, string[] terms)
        {
            string result = "";

            for (int i = 0; i < terms.Length; i++)
            {
                result += terms[i] + ": ";

                foreach (var oSearchResult in lstSearchResults.FindAll(x => x.Query == terms[i]))
                {
                    result += oSearchResult.SearchEngine.GetResultsDetail(oSearchResult.Amount) + " ";
                }
                result += Environment.NewLine;
            }
            return result;
        }

        public string Winners(List<SearchResult> lstSearchResults, List<ISearchEngine> lstSearchEngine)
        {
            string result = "";
            string winner;

            foreach (var oSearchEngine in lstSearchEngine)
            {
                lstSearchResults.Sort((p, q) => p.Amount.CompareTo(q.Amount));
                winner = lstSearchResults.FindLast(x => x.SearchEngine.Name == oSearchEngine.Name).Query;
                result += oSearchEngine.Name + " winner: " + winner + Environment.NewLine;
            }
            return result;
        }

        public string TotalWinner(List<SearchResult> lstSearchResults, string[] terms)
        {
            var myList = new List<KeyValuePair<string, long>>();

            long total;
            string result;

            for (int i = 0; i < terms.Length; i++)
            {
                total = lstSearchResults.Where(x => x.Query == terms[i]).Sum(x => x.Amount);
                myList.Add(new KeyValuePair<string, long>(terms[i], total));
            }

            result = "Total winner: " + myList.OrderByDescending(t => t.Value).First().Key;

            return result;
        }
    }
}

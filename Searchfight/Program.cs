using Searchfight.BE;
using Searchfight.Interfaces;
using System;
using System.Collections.Generic;

namespace Searchfight
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                System.Console.WriteLine("You haven´t enter any word or term");
            }
            else
            {
                List<ISearchEngine> lstSearchEngine = new List<ISearchEngine>();
                ISearchEngine searchEngineG = new Google();
                ISearchEngine searchEngineB = new Bing();
                lstSearchEngine.Add(searchEngineG);
                lstSearchEngine.Add(searchEngineB);

                ShowResults(lstSearchEngine, args);
            }            
        }

        static void ShowResults(List<ISearchEngine> lstSearchEngine, string[] terms)
        {
            try
            {
                FinalResult finalResult = new FinalResult();
                List<SearchResult> lstSearchResults = new List<SearchResult>();
                SearchResult objSearchResult = new SearchResult();
                lstSearchResults = objSearchResult.GetSearchResults(lstSearchEngine, terms);

                Console.Write(finalResult.ResultDetails(lstSearchResults, terms));
                Console.Write(finalResult.Winners(lstSearchResults, lstSearchEngine));
                Console.Write(finalResult.TotalWinner(lstSearchResults, terms));
            }
            catch (Exception ex)
            {
                Console.Write("There was an error: " + ex.Message);
            }
        }
    }
}

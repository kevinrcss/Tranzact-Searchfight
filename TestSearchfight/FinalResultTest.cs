using Microsoft.VisualStudio.TestTools.UnitTesting;
using Searchfight.BE;
using Searchfight.Interfaces;
using System;
using System.Collections.Generic;

namespace TestSearchfight
{
    [TestClass]
    public class FinalResultTest
    {
        FinalResult finalResult = new FinalResult();
        List<SearchResult> lstSearchResults = new List<SearchResult>();
        SearchResult searchResultG1 = new SearchResult(new Google(), ".net", 500);
        SearchResult searchResultG2 = new SearchResult(new Google(), "java", 400);
        SearchResult searchResultB1 = new SearchResult(new Bing(), ".net", 390);
        SearchResult searchResultB2 = new SearchResult(new Bing(), "java", 410);
        string[] terms = new string[] { ".net", "java" };
        List<ISearchEngine> lstSearchEngine = new List<ISearchEngine>();
        ISearchEngine searchEngineG = new Google();
        ISearchEngine searchEngineB = new Bing();

        [TestMethod]
        public void ResultDetails()
        {
            string result;
            string expected;
            lstSearchResults.Add(searchResultG1);
            lstSearchResults.Add(searchResultG2);
            lstSearchResults.Add(searchResultB1);
            lstSearchResults.Add(searchResultB2);
            expected = ".net: Google: 500 Bing: 390 " + Environment.NewLine +
                        "java: Google: 400 Bing: 410 " + Environment.NewLine;

            result = finalResult.ResultDetails(lstSearchResults, terms);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Winners()
        {
            string result;
            string expected;
            lstSearchResults.Add(searchResultG1);
            lstSearchResults.Add(searchResultG2);
            lstSearchResults.Add(searchResultB1);
            lstSearchResults.Add(searchResultB2);
            lstSearchEngine.Add(searchEngineG);
            lstSearchEngine.Add(searchEngineB);
            expected = "Google winner: .net" + Environment.NewLine +
                "Bing winner: java" + Environment.NewLine;

            result = finalResult.Winners(lstSearchResults, lstSearchEngine);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TotalWinner()
        {
            string result;
            string expected;
            lstSearchResults.Add(searchResultG1);
            lstSearchResults.Add(searchResultG2);
            lstSearchResults.Add(searchResultB1);
            lstSearchResults.Add(searchResultB2);
            expected = "Total winner: .net";

            result = finalResult.TotalWinner(lstSearchResults, terms);

            Assert.AreEqual(expected, result);

        }
    }
}

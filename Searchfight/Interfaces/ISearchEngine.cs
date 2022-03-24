using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searchfight.Interfaces
{
    public interface ISearchEngine
    {
        public string Name { get; set; }
        public long GetSearchResults(string query);
        public string GetResultsDetail(long results);
    }
}

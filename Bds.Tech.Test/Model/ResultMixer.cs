using Bds.Tech.Test.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bds.Tech.Test.Model
{
    public class ResultMixer
    {
        IList<ISearchEngine> searchEngines = new List<ISearchEngine>();

        public void RegisterSearchEngine(ISearchEngine searchEngine)
        {
            searchEngines.Add(searchEngine);
        }

        public async Task<IEnumerable<Result>> GetCombinedSearchResultsAsync(string searchTerm)
        {
            IList<IList<Result>> resultSets = new List<IList<Result>>();
            foreach (var engine in searchEngines)
            {
                var res = await engine.GetResultsAsync(searchTerm);
                resultSets.Add(res);
            }

            return CombineResults(resultSets);
        }

        private IEnumerable<Result> CombineResults(IList<IList<Result>> resultSets)
        {
            var maxLength = (from i in resultSets select i.Count).Max();
            for (int i = 0; i < maxLength; i++)
            {
                foreach (var results in resultSets)
                {
                    var element = results.ElementAtOrDefault(i);
                    if (element != null)
                    {
                        yield return element;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
    }
}

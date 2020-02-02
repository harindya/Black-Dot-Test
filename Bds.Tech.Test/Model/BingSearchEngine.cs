using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bds.Tech.Test.Model
{
    public class BingSearchEngine : SearchEngineBase
    {
        public override async Task<IList<Result>> GetResultsAsync(string searchTerm)
        {
            var resultHtml = await GetHtml(GetSearchUrl(searchTerm));
            var results = await UsingLinq(resultHtml);

            return results;
        }

        protected override string GetSearchUrl(string searchTerm)
        {
            string baseString = @"https://www.bing.com/search?q=";
            StringBuilder result = new StringBuilder();
            result.Append(baseString);
            result.Append(searchTerm.Replace(" ", "+"));
            return result.ToString();
        }

        static async Task<IList<Result>> UsingLinq(string content)
        {
            var context = BrowsingContext.New(Configuration.Default);

            var document = await context.OpenAsync(req => req.Content(content));

            var listItemsCssSelector = document.QuerySelectorAll("li[class='b_algo']");

            IList<IElement> searchResults = new List<IElement>();
            foreach (var item in listItemsCssSelector)
            {
                searchResults.Add(item);
            }

            IList<Result> results = new List<Result>();
            foreach (var result in searchResults)
            {
                var aElement = result.QuerySelector("h2 > a");
                var url = aElement?.GetAttribute("href");

                var title = aElement?.InnerHtml;

                var previewElement = result.QuerySelector("p");
                var preview = previewElement?.InnerHtml;

                if (url != null && title != null)
                {
                    results.Add(new Result { Title = title, Url = url, Text = preview, SourceSearchEngine = nameof(BingSearchEngine) });
                }
            }

            return results;
        }
    }
}

using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bds.Tech.Test.Model
{
    public class GoogleSearchEngine : SearchEngineBase
    {
        public override async Task<IList<Result>> GetResultsAsync(string searchTerm)
        {
            var resultHtml = await GetHtml(GetSearchUrl(searchTerm));
            var results = await UsingLinq(resultHtml);

            return results;
        }

        protected override  string GetSearchUrl(string searchTerm)
        {
            string baseString = @"https://www.google.com/search?q=";
            StringBuilder result = new StringBuilder();
            result.Append(baseString);
            result.Append(searchTerm.Replace(" ", "+"));
            return result.ToString();
        }       

               
        static async Task<IList<Result>> UsingLinq(string content)
        {
            var context = BrowsingContext.New(Configuration.Default);

            var document = await context.OpenAsync(req => req.Content(content));

            var divItemsCssSelector = document.QuerySelectorAll("div[class='ZINbbc xpd O9g5cc uUPGi']");

            IList<IElement> searchResults = new List<IElement>();
            foreach (var item in divItemsCssSelector)
            {
                var subElements = item.QuerySelectorAll("div[class='kCrYT'] > a");
                foreach (var element in subElements)
                {
                    searchResults.Add(element.ParentElement.ParentElement);
                }
            }

            IList<Result> results = new List<Result>();
            foreach (var result in searchResults)
            {
                var aElement = result.QuerySelector("a");
                var url = aElement?.GetAttribute("href")?.Replace(@"/url?q=", string.Empty);

                var titleElement = result.QuerySelector("div[class='BNeawe vvjwJb AP7Wnd']");
                var title = titleElement?.InnerHtml;

                var previewElement = result.QuerySelector("div[class='BNeawe s3v9rd AP7Wnd'] div[class='BNeawe s3v9rd AP7Wnd']");
                foreach (var element in previewElement.QuerySelectorAll("span"))
                {
                    element.Replace(element.ChildNodes.ToArray());
                }

                var preview = previewElement?.InnerHtml;

                if (url != null && title != null)
                {
                    results.Add(new Result { Title = title, Url = url, Text = preview, SourceSearchEngine = nameof(GoogleSearchEngine) });
                }
            }

            return results;
        }
        
    }
}

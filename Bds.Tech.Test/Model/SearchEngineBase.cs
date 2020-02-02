using Bds.Tech.Test.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bds.Tech.Test.Model
{
    public abstract class SearchEngineBase : ISearchEngine
    {
        public abstract Task<IList<Result>> GetResultsAsync(string searchTerm);       

        protected abstract string GetSearchUrl(string searchTerm);       


        protected async Task<string> GetHtml(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                string rt = await GetHtmlResponse(request);

                return rt;
            }

            catch (Exception ex)
            {
                throw new BlackDotTestException("Error: " + ex.Message, ex);
            }
        }

        private async Task<string> GetHtmlResponse(WebRequest request)
        {
            return await Task.Run(() =>
            {
                string rt;
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        rt = reader.ReadToEnd();
                    }
                }

                return rt;
            });

        }
    }
}

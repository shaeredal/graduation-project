using System.IO;
using System.Net;
using Newtonsoft.Json;
using OAuth2.Infrastructure;
using OnlinerNotifier.BLL.Models.SearchDataModels;
using OnlinerNotifier.BLL.Services.Interfaces.SearchServices;

namespace OnlinerNotifier.BLL.Services.Implementations.SearchServices
{
    public class OnlinerSearchService : IOnlinerSearchService
    {
        public SearchResult Search(string productName)
        {
            var searchResultString = MakeRequest(productName);
            return ParseResponse(searchResultString);
        }

        private string MakeRequest(string productName)
        {
            var requestString = $"https://catalog.api.onliner.by/search/products?query={productName}";
            var request = (HttpWebRequest)WebRequest.Create(requestString);
            request.Method = "GET";
            request.Accept = "application/json";
            var response = (HttpWebResponse)request.GetResponse();
            return ReadResponseStream(response.GetResponseStream());
        }

        private string ReadResponseStream(Stream responseStream)
        {
            if (responseStream != null)
            {
                using (var reader = new StreamReader(responseStream))
                {
                    return reader.ReadToEnd();
                }
            }
            return null;
        }

        private SearchResult ParseResponse(string searchResultString)
        {
            var result = JsonConvert.DeserializeObject<SearchResult>(searchResultString);
            result.Products.ForEach(x => x.CatalogName = "onliner.by");
            return result;
        }
    }
}

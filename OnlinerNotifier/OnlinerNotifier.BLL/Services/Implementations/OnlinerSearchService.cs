using System.IO;
using System.Net;
using Newtonsoft.Json;
using OnlinerNotifier.BLL.Models.OnlinerDataModels;
using OnlinerNotifier.BLL.Services.Interfaces;

namespace OnlinerNotifier.BLL.Services.Implementations
{
    public class OnlinerSearchService : IOnlinerSearchService
    {
        public SearchResultOnliner Search(string productName)
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

        private SearchResultOnliner ParseResponse(string searchResultString)
        {
            return JsonConvert.DeserializeObject<SearchResultOnliner>(searchResultString);
        }
    }
}

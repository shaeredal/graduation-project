using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using OnlinerNotifier.BLL.Mappers.Interfaces;
using OnlinerNotifier.BLL.Models.SearchDataModels;
using OnlinerNotifier.BLL.Services.Interfaces.SearchServices;

namespace OnlinerNotifier.BLL.Services.Implementations.SearchServices
{
    public class KupiTutBySearchService : IKupiTutBySearchService
    {
        private readonly IKupiTutByProductMapper kupiTutByProductMapper;

        public KupiTutBySearchService(IKupiTutByProductMapper kupiTutByProductMapper)
        {
            this.kupiTutByProductMapper = kupiTutByProductMapper;
        }

        public SearchResult Search(string productName)
        {
            var searchResultString = MakeRequest(productName);
            return ParseResponse(searchResultString);
        }

        private string MakeRequest(string productName)
        {
            var requestString = $"https://kupi.tut.by/search/suggests.json?part={productName}";
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
            searchResultString = searchResultString.Replace("&thinsp;", "");
            searchResultString = searchResultString.Replace("false", "null");
            var result = JsonConvert.DeserializeObject<IEnumerable<KupiTutByProduct>>(searchResultString).Where(x => x.IdType == "m")
                .Select(kupiTutByProductMapper.ToSearchProduct);
            return new SearchResult
            {
                Products = result
            };
        }
    }
}

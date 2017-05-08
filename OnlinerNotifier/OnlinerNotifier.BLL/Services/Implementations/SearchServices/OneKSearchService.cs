using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using OnlinerNotifier.BLL.Models.SearchDataModels;
using OnlinerNotifier.BLL.Services.Interfaces.SearchServices;
using CsQuery;

namespace OnlinerNotifier.BLL.Services.Implementations.SearchServices
{
    public class OneKSearchService : IOneKSearchService
    {
        public SearchResult Search(string productName)
        {
            var searchResultString = MakeRequest(productName);
            return ParseResponse(searchResultString);
        }

        private string MakeRequest(string productName)
        {
            var requestString = $"http://1k.by/products/searchautocomplete?term={productName}";
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
            var result = JsonConvert.DeserializeObject<IEnumerable<OneKProduct>>(searchResultString)
                .Where(x =>
                {
                    var html = new CQ(x.Label);
                    var idString = html[".b-pr-line-m"].Attr("data-productid");
                    return int.TryParse(idString, out int _);
                })
                .Select(searchProduct =>
                {
                    var html = new CQ(searchProduct.Label);
                    var minPriceString = html[".pr-price_cash .pr-price_mark"]
                        .Text()
                        .Replace("от ", "")
                        .Replace(" б.р.", "")
                        .Replace(" ", "");
                    decimal? minPrice;
                    if (decimal.TryParse(minPriceString, out decimal d))
                    {
                        minPrice = decimal.Parse(minPriceString);
                    }
                    else
                    {
                        minPrice = null;
                    }
                    var image = html[".pr-line_pic img"].Attr("src");
                    var url = html[".pr-line_link"].Attr("href");
                    var idString = html[".b-pr-line-m"].Attr("data-productid");
                    var id = int.Parse(idString);
                    return new SearchProduct
                    {
                        CatalogName = "1k.by",
                        FullName = searchProduct.Value,
                        Id = id,
                        PhotoUrl = image,
                        Prices = new SearchPrice
                        {
                            Min = minPrice * 10000
                        },
                        Url = url
                    };
                });
            return new SearchResult
            {
                Products = result
            };
        }
    }
}

using Newtonsoft.Json;

namespace OnlinerNotifier.BLL.Models.SearchDataModels
{
    public class SearchProduct
    {
        public int Id { get; set; }

        public string CatalogName { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        public SearchPrice Prices { get; set; }

        public string PhotoUrl { get; set; }

        public string Url { get; set; }
    }
}

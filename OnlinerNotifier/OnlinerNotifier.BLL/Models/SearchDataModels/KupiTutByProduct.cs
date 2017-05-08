using System.Collections.Generic;
using Newtonsoft.Json;

namespace OnlinerNotifier.BLL.Models.SearchDataModels
{
    public class KupiTutByProduct
    {
        public string Name { get; set; }

        [JsonProperty("price_min")]
        public decimal? PriceMin { get; set; }

        [JsonProperty("photo_url")]
        public string PhotoUrl { get; set; }

        public string Url { get; set; }

        public int Id { get; set; }

        [JsonProperty("id_type")]
        public string IdType { get; set; }
    }
}

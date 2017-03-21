using Newtonsoft.Json;

namespace OnlinerNotifier.BLL.Models.OnlinerDataModels
{
    public class ProductOnliner
    {
        public int Id { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        public PriceOnliner Prices { get; set; }
    }
}

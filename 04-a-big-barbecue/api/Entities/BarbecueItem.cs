using Newtonsoft.Json;

namespace BarbecueCost.Api.Entities
{
    public class BarbecueItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cost")]
        public double? Cost { get; set; }

        [JsonProperty("quantity")]
        public double? Quantity { get; set; }

        [JsonProperty("quantityUnit")]
        public string QuantityUnit { get; set; }
    }
}
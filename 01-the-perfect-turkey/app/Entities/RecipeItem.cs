using Newtonsoft.Json;

namespace app.Entities
{
    public class RecipeItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("order")]
        public int Order { get; set; }
    }
}
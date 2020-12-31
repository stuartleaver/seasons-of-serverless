using Newtonsoft.Json;

namespace MagicChocolateBox.Chocolates
{
    public class SelectedChocolate
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
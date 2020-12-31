using Newtonsoft.Json;

namespace MagicChocolateBox.Chocolates
{
    public class Chocolate
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        public void Subtract(int quantity)
        {
            Quantity = Quantity - quantity;
        }
    }
}
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BarbecueCost.Api.Entities
{
    public class BarbecueDetails
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("guests")]
        public int Guests { get; set; }

        [JsonProperty("budget")]
        public int Budget { get; set; }

        [JsonProperty("barbecueItems")]
        public List<BarbecueItem> BarbecueItems { get; set; }
    }
}
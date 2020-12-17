using System.Collections.Generic;
using Newtonsoft.Json;

namespace BarbecueCost.Api.Entities
{
    public class BarbequeCostResult
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("barbequeItemsTotalCost")]
        public double? BarbequeItemsTotalCost { get; set; }

        [JsonProperty("costPerPerson")]
        public double? CostPerPerson { get; set; }

        [JsonProperty("remainingBudget")]
        public double? RemainingBudget { get; set; }

        [JsonProperty("barbecueItems")]
        public List<BarbecueItem> BarbecueItems { get; set; }
    }
}
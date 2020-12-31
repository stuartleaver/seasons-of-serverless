using System;
using Newtonsoft.Json;

namespace MagicChocolateBox.Api.Entities
{
    public class Reservation
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("chocolateName")]
        public string ChocolateName { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("reservedDateTime")]
        public DateTime ReservedDateTime { get; set; }
    }
}
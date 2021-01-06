using Newtonsoft.Json;

namespace RecipeNotifier.Entities
{
    public class RegionalCenterInformation
    {
        [JsonProperty("countrySubdivision")]
        public string CountrySubdivision { get; set; }

        [JsonProperty("municipality")]
        public string Municipality { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
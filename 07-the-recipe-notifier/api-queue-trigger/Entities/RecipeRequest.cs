using Newtonsoft.Json;

namespace RecipeNotifier.Entities
{
    public class RecipeRequest
    {
        [JsonProperty("countrySubdivision")]
        public string CountrySubdivision { get; set; }

        [JsonProperty("municipality")]
        public string Municipality { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
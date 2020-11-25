using Newtonsoft.Json;

namespace PerfectHolidayTurkey.Api
{
    public class Recipe
    {
        [JsonProperty("turkeyWeight")]
        public decimal TurkeyWeight {get; set;}

        [JsonProperty("salt")]
        public decimal Salt => 0.05M * TurkeyWeight;

        [JsonProperty("water")]
        public decimal Water => 0.66M * TurkeyWeight;

        [JsonProperty("brownSugar")]
        public decimal BrownSugar => 0.13M * TurkeyWeight;

        [JsonProperty("shallots")]
        public decimal Shallots => 0.2M * TurkeyWeight;

        [JsonProperty("clovesOfGarlic")]
        public decimal ClovesOfGarlic => 0.4M * TurkeyWeight;

        [JsonProperty("wholePeppercorns")]
        public decimal WholePeppercorns => 0.13M * TurkeyWeight;

        [JsonProperty("driedJuniperBerries")]
        public decimal DriedJuniperBerries => 0.13M * TurkeyWeight;

        [JsonProperty("freshRosemary")]
        public decimal FreshRosemary => 0.13M * TurkeyWeight;

        [JsonProperty("thyme")]
        public decimal Thyme => 0.06M * TurkeyWeight;

        [JsonProperty("brineTime")]
        public decimal BrineTime => 2.4M * TurkeyWeight;

        [JsonProperty("roastTime")]
        public decimal RoastTime => 15M * TurkeyWeight;
    }
}
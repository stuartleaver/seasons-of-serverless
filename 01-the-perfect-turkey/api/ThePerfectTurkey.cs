using System.Collections.Generic;
using Newtonsoft.Json;
using PerfectHolidayTurkey.Api.Entities;

namespace PerfectHolidayTurkey.Api
{
    public class ThePerfectTurkey
    {
        [JsonProperty("turkeyWeight")]
        public decimal TurkeyWeight { get; set; }

        public List<RecipeItem> Recipe()
        {
            var recipe = new List<RecipeItem> {
                new RecipeItem { Name = "Salt", Amount = 0.05M * TurkeyWeight, Unit = ((Units)Units.Cups).GetEnumDescription(), Order = 1},
                new RecipeItem { Name = "Water", Amount = 0.66M * TurkeyWeight, Unit = ((Units)Units.Gallons).GetEnumDescription(), Order = 2},
                new RecipeItem { Name = "Brown sugar", Amount = 0.13M * TurkeyWeight, Unit = ((Units)Units.Cups).GetEnumDescription(), Order = 3},
                new RecipeItem { Name = "Shallots", Amount = 0.2M * TurkeyWeight, Unit = "Whole", Order = 4},
                new RecipeItem { Name = "Cloves of garlic", Amount = 0.4M * TurkeyWeight, Unit = "Whole", Order = 5},
                new RecipeItem { Name = "Whole peppercorns", Amount = 0.13M * TurkeyWeight, Unit = ((Units)Units.Tablespoons).GetEnumDescription(), Order = 6},
                new RecipeItem { Name = "Dried juniper berries", Amount = 0.13M * TurkeyWeight, Unit = ((Units)Units.Tablespoons).GetEnumDescription(), Order = 7},
                new RecipeItem { Name = "Fresh rosemary", Amount = 0.13M * TurkeyWeight, Unit = ((Units)Units.Tablespoons).GetEnumDescription(), Order = 8},
                new RecipeItem { Name = "Thyme", Amount = 0.06M * TurkeyWeight, Unit = ((Units)Units.Tablespoons).GetEnumDescription(), Order = 9},
                new RecipeItem { Name = "Brine time", Amount = 2.4M * TurkeyWeight, Unit = ((Units)Units.Hours).GetEnumDescription(), Order = 10},
                new RecipeItem { Name = "Roast time", Amount = 15M * TurkeyWeight, Unit = ((Units)Units.Minutes).GetEnumDescription(), Order = 11}
            };

            return recipe;
        }
    }
}
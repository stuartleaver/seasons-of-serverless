using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace MagicChocolateBox.Chocolates
{
    public abstract class ChocolateBox
    {
        [JsonProperty("chocolateBoxSize")]
        public ChocolateBoxSize ChocolateBoxSize;

        [JsonProperty("chocolates")]
        public List<Chocolate> Chocolates;

        [JsonProperty("remainingChocolates")]
        public int RemainingChocolates => Chocolates.Sum(x => x.Quantity);

        public ChocolateBox(ChocolateBoxSize chocolateBoxSize)
        {
            ChocolateBoxSize = chocolateBoxSize;

            Chocolates = new List<Chocolate>();
        }

        public abstract void Create();
    }
}
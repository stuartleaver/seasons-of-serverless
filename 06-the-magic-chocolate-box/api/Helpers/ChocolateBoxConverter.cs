using System;
using MagicChocolateBox.Chocolates;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MagicChocolateBox.Helpers
{
    public class ChocolateBoxConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType) => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);

            if (jsonObject["chocolateBoxSize"].Value<int>() == (int)ChocolateBoxSize.Small)
            {
                return jsonObject.ToObject<SmallChocolateBox>(serializer);
            }
            else if (jsonObject["chocolateBoxSize"].Value<int>() == (int)ChocolateBoxSize.Medium)
            {
                return jsonObject.ToObject<MediumChocolateBox>(serializer);
            }
            else if (jsonObject["chocolateBoxSize"].Value<int>() == (int)ChocolateBoxSize.Large)
            {
                return jsonObject.ToObject<LargeChocolateBox>(serializer);
            }

            return serializer.Deserialize<T>(reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Tests.Helpers
{
    public class RegionInfoConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var regionInfo = value as RegionInfo;
            if (regionInfo != null)
                writer.WriteValue(regionInfo.ToString());
            else
                writer.WriteNull();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return IsValidRegionInfo(reader.Value as string, out var regionInfo) ? regionInfo : null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(RegionInfo);
        }

        private static bool IsValidRegionInfo(string text, out RegionInfo value)
        {
            try
            {
                value = new RegionInfo(text);
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }
    }
}
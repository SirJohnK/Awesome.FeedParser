using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Tests.Helpers
{
    /// <summary>
    /// Custom json converter for System.Globalization.RegionInfo.
    /// </summary>
    public class RegionInfoConverter : JsonConverter
    {
        /// <summary>
        /// Custom serializer for RegionInfo.
        /// </summary>
        /// <param name="writer">The JsonWriter to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var regionInfo = value as RegionInfo;
            if (regionInfo != null)
                writer.WriteValue(regionInfo.ToString());
            else
                writer.WriteNull();
        }

        /// <summary>
        /// Custom deserializer for RegionInfo.
        /// </summary>
        /// <param name="reader">The JsonReader to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The deserialized object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return IsValidRegionInfo(reader.Value as string, out var regionInfo) ? regionInfo : null;
        }

        /// <summary>
        /// Determines whether the specified object type is RegionInfo.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>true if the specified object type is RegionInfo, otherwise false.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(RegionInfo);
        }

        /// <summary>
        /// Validates if specified text can be converted to RegionInfo.
        /// </summary>
        /// <param name="text">Text to validate.</param>
        /// <param name="value">Output RegionInfo.</param>
        /// <returns>true if specified text can be converted to RegionInfo, otherwise false.</returns>
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
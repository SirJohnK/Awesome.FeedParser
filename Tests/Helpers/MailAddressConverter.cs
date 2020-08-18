using Newtonsoft.Json;
using System;
using System.Net.Mail;

namespace Tests.Helpers
{
    /// <summary>
    /// Custom json converter for System.Net.Mail.MailAddress.
    /// </summary>
    public class MailAddressConverter : JsonConverter
    {
        /// <summary>
        /// Custom serializer for MailAddress.
        /// </summary>
        /// <param name="writer">The JsonWriter to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var mailAddress = value as MailAddress;
            if (mailAddress != null)
                writer.WriteValue(mailAddress.ToString());
            else
                writer.WriteNull();
        }

        /// <summary>
        /// Custom deserializer for MailAddress.
        /// </summary>
        /// <param name="reader">The JsonReader to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The deserialized object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return IsValidMailAddress(reader.Value as string, out var mailAddress) ? mailAddress : null;
        }

        /// <summary>
        /// Determines whether the specified object type is MailAddress.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>true if the specified object type is MailAddress, otherwise false.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(MailAddress);
        }

        /// <summary>
        /// Validates if specified text can be converted to MailAdress.
        /// </summary>
        /// <param name="text">Text to validate.</param>
        /// <param name="value">Output MailAddress.</param>
        /// <returns>true if specified text can be converted to MailAddress, otherwise false.</returns>
        private static bool IsValidMailAddress(string text, out MailAddress value)
        {
            try
            {
                value = new MailAddress(text);
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
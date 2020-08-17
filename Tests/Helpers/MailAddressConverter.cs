using Newtonsoft.Json;
using System;
using System.Net.Mail;

namespace Tests.Helpers
{
    public class MailAddressConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var mailAddress = value as MailAddress;
            if (mailAddress != null)
                writer.WriteValue(mailAddress.ToString());
            else
                writer.WriteNull();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return IsValidMailAddress(reader.Value as string, out var mailAddress) ? mailAddress : null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(MailAddress);
        }

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
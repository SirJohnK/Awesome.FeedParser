using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Tests.Helpers
{
    internal class JsonCustomResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty result = base.CreateProperty(member, memberSerialization);

            //If interface, attempt to get backing field
            if (result.PropertyType.IsInterface)
            {
                var privateName = char.ToLowerInvariant(result.PropertyName[0]) + result.PropertyName.Substring(1);
                var privateField = member.DeclaringType.GetRuntimeFields().FirstOrDefault(x => x.Name.Equals(privateName));

                if (privateField != null)
                {
                    var originalPropertyName = result.PropertyName;
                    result = base.CreateProperty(privateField, memberSerialization);
                    result.Writable = true;
                    result.PropertyName = originalPropertyName;
                    result.UnderlyingName = originalPropertyName;
                    result.Readable = true;
                }
                else
                {
                    Debug.WriteLine($"JsonCustomResolver: Backing field for {result.DeclaringType.Name}.{result.PropertyName} not found!");
                }
            }
            else
            {
                var propInfo = member as PropertyInfo;
                result.Readable |= propInfo != null && propInfo.CanRead;
                result.Writable |= propInfo != null && propInfo.CanWrite;
            }

            return result;
        }
    }
}
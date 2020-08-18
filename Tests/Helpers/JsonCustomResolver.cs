using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Tests.Helpers
{
    /// <summary>
    /// Custom json resolver to override internal, private and interface properties to enable verification tests.
    /// </summary>
    internal class JsonCustomResolver : DefaultContractResolver
    {
        /// <summary>
        /// Override and update json property handling to be able to read and write the property.
        /// </summary>
        /// <param name="member">The member to create a JsonProperty for.</param>
        /// <param name="memberSerialization">The member's parent MemberSerialization.</param>
        /// <returns></returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            //Init
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
                //If possible, overrride read and write json property settings
                var propInfo = member as PropertyInfo;
                result.Readable |= propInfo != null && propInfo.CanRead;
                result.Writable |= propInfo != null && propInfo.CanWrite;
            }

            //Return updated property
            return result;
        }
    }
}
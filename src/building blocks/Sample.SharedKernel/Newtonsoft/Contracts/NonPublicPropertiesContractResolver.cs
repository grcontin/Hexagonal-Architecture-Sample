using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection;

namespace Sample.SharedKernel.Newtonsoft.Contracts
{
    public class NonPublicPropertiesContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (!property.Writable)
            {
                var propertyInfo = member as PropertyInfo;
                if (propertyInfo != null)
                {
                    var hasNonPublicSetter = propertyInfo.GetSetMethod(true) != null;
                    property.Writable = hasNonPublicSetter;
                }
            }

            return property;
        }
    }
}

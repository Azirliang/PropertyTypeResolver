using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProperyTypeResolver.Abstractions;

namespace PropertyTypeResolver.Core
{
    public class PropertyTypeConverter : JsonCreationConverter<IPropertyType>
    {
        private readonly IServiceProvider _serviceProvider;
        public PropertyTypeConverter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override IPropertyType Create(Type objectType, JObject jObject)
        {
            JToken? jToken = jObject.GetValue("propertytype", StringComparison.InvariantCultureIgnoreCase) ?? JsonConvert.DeserializeObject<JToken>("");
            var typeName = jToken?.ToString();

            if (typeName == null)
                throw new ArgumentException($"No such property type: {typeName}", nameof(typeName));

            IPropertyTypeResolver typeResolver = _serviceProvider.GetRequiredService<IPropertyTypeResolver>();
            var type = typeResolver.ResolveProperty(typeName);

            if (type == null)
                throw new ArgumentException($"No such property type: {typeName}", nameof(typeName));

            return type;
        }
    }
}

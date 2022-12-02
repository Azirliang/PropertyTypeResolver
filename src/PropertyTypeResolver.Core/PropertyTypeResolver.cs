using Microsoft.Extensions.DependencyInjection;
using ProperyTypeResolver.Abstractions;

namespace PropertyTypeResolver.Core
{
    public class PropertyTypeResolver : IPropertyTypeResolver
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Lazy<IDictionary<string, IPropertyType>> _lazyPropertyTypeLookup;

        public PropertyTypeResolver(IServiceProvider serviceProvider, Func<IEnumerable<IPropertyType>> activitiesFunc)
        {
            _serviceProvider = serviceProvider;
            _lazyPropertyTypeLookup = new Lazy<IDictionary<string, IPropertyType>>(
                () =>
                {
                    var activities = activitiesFunc();
                    var types = activities.Select(x => x.GetType()).Distinct();
                    var propertyTypes = new Dictionary<string, IPropertyType>();
                    foreach (var type in types)
                    {
                        IPropertyType propertyType = (IPropertyType)ActivatorUtilities.GetServiceOrCreateInstance(_serviceProvider, type);
                        propertyTypes[propertyType.PropertyType] = propertyType;
                    }
                    return propertyTypes;
                });
        }

        private IDictionary<string, IPropertyType> PropertyTypeLookup => _lazyPropertyTypeLookup.Value;

        public IPropertyType? ResolveProperty(string propertyTypeName)
        {
            if (!PropertyTypeLookup.ContainsKey(propertyTypeName))
            {
                return null;
            }

            return PropertyTypeLookup[propertyTypeName];
        }

        public Type? GetPropertyType(string propertyTypeName)
        {
            if (!PropertyTypeLookup.ContainsKey(propertyTypeName))
            {
                return null;
            }

            return PropertyTypeLookup[propertyTypeName].GetType();
        }
    }
}

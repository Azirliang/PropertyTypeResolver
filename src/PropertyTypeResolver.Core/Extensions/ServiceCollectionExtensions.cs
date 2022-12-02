using Microsoft.Extensions.DependencyInjection.Extensions;
using ProperyTypeResolver.Abstractions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPropertyType<T>(this IServiceCollection services) where T : class, IPropertyType
        {
            return services
                .AddTransient<T>()
                .AddTransient<IPropertyType>(sp => sp.GetRequiredService<T>());
        }

        public static IServiceCollection AddPropertyTypeResolver(this IServiceCollection services)
        {
            services.AddTransient<Func<IEnumerable<IPropertyType>>>(sp => sp.GetServices<IPropertyType>);
            services.TryAddSingleton<IPropertyTypeResolver, PropertyTypeResolver.Core.PropertyTypeResolver>();

            return services;
        }
    }
}

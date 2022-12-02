using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace PropertyTypeResolver.Core
{
    public class ConfigurePropertyTypeJsonOptions : IConfigureOptions<MvcNewtonsoftJsonOptions>
    {
        private readonly IServiceProvider _serviceProvider;
        public ConfigurePropertyTypeJsonOptions(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Configure(MvcNewtonsoftJsonOptions options)
        {
            options.SerializerSettings.Converters.Add(new PropertyTypeConverter(_serviceProvider));
        }
    }
}

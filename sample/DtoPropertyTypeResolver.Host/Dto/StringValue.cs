using ProperyTypeResolver.Abstractions;

namespace DtoPropertyTypeResolver.Dto
{
    public class StringValue : IPropertyType
    {
        public string PropertyType { get; set; } = nameof(StringValue);

        public string? Value { get; set; }
    }
}

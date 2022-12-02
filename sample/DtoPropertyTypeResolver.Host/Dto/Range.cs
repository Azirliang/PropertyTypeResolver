using ProperyTypeResolver.Abstractions;

namespace DtoPropertyTypeResolver.Dto
{
    public class RangeValue : IPropertyType
    {
        public string PropertyType { get; set; } = nameof(RangeValue);

        public decimal? minValue { get; set; }

        public decimal? maxValue { get; set; }
    }
}

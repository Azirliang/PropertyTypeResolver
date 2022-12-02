using ProperyTypeResolver.Abstractions;

namespace DtoPropertyTypeResolver.Dto
{
    public class TemplateDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public IPropertyType? TemplateValue { get; set; }
    }
}

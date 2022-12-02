using AutoMapper;
using DtoPropertyTypeResolver.Dto;
using DtoPropertyTypeResolver.Entity;
using Newtonsoft.Json;
using ProperyTypeResolver.Abstractions;

namespace DtoPropertyTypeResolver.AutoMapper
{
    public class TemplateMapper : Profile
    {
        public TemplateMapper() { }

        public TemplateMapper(IServiceProvider serviceProvider)
        {
            this.CreateMap<TemplateDto, TemplateEntity>()
                .ForMember(d => d.TemplateValue,
                m => m.MapFrom(dto =>
                dto.TemplateValue != null ? JsonConvert.SerializeObject(dto.TemplateValue) : null))
                .ForMember(d => d.PropertyType, m => m.MapFrom(dto => dto.TemplateValue != null ? dto.TemplateValue.PropertyType : null));

            this.CreateMap<TemplateEntity, TemplateDto>()
                .ForMember(d => d.TemplateValue,
                m => m.MapFrom(entity => StringToObject(entity, serviceProvider)));
        }

        public IPropertyType? StringToObject(TemplateEntity entity, IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(entity.TemplateValue))
            {
                return null;
            }

            var resolver = serviceProvider.GetService<IPropertyTypeResolver>();

            var typeName = entity.PropertyType;

            if (string.IsNullOrEmpty(typeName))
            {
                throw new ArgumentException("propertyType is empty");
            }

            var type = resolver!.GetPropertyType(typeName);
            if (type == null)
            {
                throw new ArgumentException("getType null error!");
            }
            var templateValue = JsonConvert.DeserializeObject(entity.TemplateValue, type);

            if (templateValue == null)
            {
                throw new ArgumentException("json deserializeObject error!");
            }

            return templateValue as IPropertyType;
        }
    }
}

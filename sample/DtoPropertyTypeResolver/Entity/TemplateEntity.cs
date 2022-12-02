namespace DtoPropertyTypeResolver.Entity
{
    public class TemplateEntity
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        // 也可以使用JObject读取TemplateValue里面的PropertyType 来获取类型，这边为了方便DEMO
        public string? PropertyType { get; set; }

        public string? TemplateValue { get; set; }
    }
}

namespace ProperyTypeResolver.Abstractions
{
    public interface IPropertyTypeResolver
    {
        IPropertyType? ResolveProperty(string propertyTypeName);

        Type? GetPropertyType(string propertyTypeName);
    }
}

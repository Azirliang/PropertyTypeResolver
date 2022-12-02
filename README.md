### 使用

``` C#
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddAutoMapper((sp, mapperConfiguration) =>
{
    mapperConfiguration.AddProfile(new TemplateMapper(sp));
}, typeof(Program));


builder.Services.ConfigureOptions<ConfigurePropertyTypeJsonOptions>();

builder.Services.AddPropertyType<StringValue>();
builder.Services.AddPropertyType<RangeValue>();

builder.Services.AddPropertyTypeResolver();
```



#### 示例

可以参考sample下的项目：[DEMO](sample/DtoPropertyTypeResolver.Host)

#### 演示

[sample](docs/sample.gif)

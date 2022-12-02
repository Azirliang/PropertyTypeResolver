using AutoMapper;
using DtoPropertyTypeResolver.AutoMapper;
using DtoPropertyTypeResolver.Dto;
using DtoPropertyTypeResolver.EfContext;
using Microsoft.EntityFrameworkCore;
using PropertyTypeResolver.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper((sp, mapperConfiguration) =>
{
    mapperConfiguration.AddProfile(new TemplateMapper(sp));
}, typeof(Program));

builder.Services.AddDbContext<TestContext>(options =>
{
    options.UseInMemoryDatabase("TestDatabase");
});

builder.Services.ConfigureOptions<ConfigurePropertyTypeJsonOptions>();

builder.Services.AddPropertyType<StringValue>();
builder.Services.AddPropertyType<RangeValue>();

builder.Services.AddPropertyTypeResolver();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using PropertyService.Business.Interfaces;
using PropertyService.Business.mappers;
using PropertyService.Data.Context;
using PropertyService.Data.Repositories;
using PropertyService.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<PropertyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PropertyDb") ?? throw new InvalidOperationException("Connection string 'PropertyDb' not found.")));

// AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<PropertyMappers>();
});

// Services e repository
builder.Services.AddScoped<IPropertyService, PropertyService.Business.Services.PropertyService>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IPropertyStatusHistoryRepository, PropertyStatusHistoryRepository>();

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();

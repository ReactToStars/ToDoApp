using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ToDo.API.Endpoints;
using ToDo.API.Models;
using ToDo.API.Profiles;
using ToDo.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ToDoDb")));

builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// register repositories
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

app.MapCategoryEndpoints();
app.MapTodoItemEndpoints();
app.MapUserEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Map the raw openapi/v1.json endpoint
    app.MapOpenApi();
    // Map the beautiful Scalar documentation UI (accessible at /scalar)
    app.MapScalarApiReference(); 
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

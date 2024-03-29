using ConventionServiceRegistration.Configurations;
using ConventionServiceRegistration.Extensions;
using ConventionServiceRegistration.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var configuration = builder.Configuration;

builder.BindConfigurationsWithAttributes();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterServicesWithAttributes();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/get", ([FromServices] IServiceOne serviceOne, [FromServices] IServiceTwo serviceTwo, [FromServices]  IServiceThree serviceThree) =>
{
    var serviceOneMessage = serviceOne.GetMessage();
    var serviceTwoMessage = serviceTwo.GetMessage();
    var serviceThreeMessage = serviceThree.GetMessage();

    var messages = new string[] { serviceOneMessage, serviceTwoMessage, serviceThreeMessage };

    return messages;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

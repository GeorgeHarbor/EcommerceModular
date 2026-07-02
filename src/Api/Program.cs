using Api.ExceptionHandlers;
using Catalog.Api;
using Catalog.Api.Endpoints;
using Catalog.Application;
using Catalog.Infrastructure;
using SharedKernel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddProblemDetails();

IModule[] modules = [new CatalogModule()];

foreach (var module in modules)
    module.RegisterServices(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

foreach (var module in modules)
    module.MapEndpoints(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

using Catalog.Application.Abstractions.Behaviours;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application;

public static class DependencyInjection
{
   public static IServiceCollection AddCatalogApplication(this IServiceCollection services)
   {
      services.AddMediatR(config =>
      {
         config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
         config.AddOpenBehavior(typeof(ValidationBehavior<,>));
      });

      services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

      return services;
   }
}
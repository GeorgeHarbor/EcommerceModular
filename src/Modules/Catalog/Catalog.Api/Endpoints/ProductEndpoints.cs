using Catalog.Application.Products.Commands;
using Catalog.Application.Products.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharedKernel.Results;

namespace Catalog.Api.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products").WithTags("Products");

        group.MapPost("/", async (CreateProductCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);

            return result.IsSuccess
                ? Results.Created($"/products/{result.Value}", result.Value)
                : Results.BadRequest(new { error = result.Error.Code, message = result.Error.Description });
        });

        group.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(new {error = result.Error.Code, message = result.Error.Description});
        });

    }

}
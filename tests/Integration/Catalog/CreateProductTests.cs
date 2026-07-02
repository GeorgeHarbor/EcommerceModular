using System.Net;
using System.Net.Http.Json;
using Catalog.Application.Products.GetProductById;
using Catalog.Domain.ValueObjects;
using Catalog.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;

namespace Integration.Catalog;

public class CreateProductTests(IntegrationTestWebAppFactory factory) : IntegrationTestBase(factory)
{
   [Fact]
   public async Task CreateProduct_WhenValid_ShouldReturn201AndPersistToDatabase()
   {
      // Arrange
      var request = new
      {
         name = "iPhone 15",
         price = 999.99m,
         currency = "EUR",
      };
      
      // Act
       var response = await Client.PostAsJsonAsync("/products", request);
       
       // Assert - HTTP
       response.StatusCode.Should().Be(HttpStatusCode.Created);

       var productId = await response.Content.ReadFromJsonAsync<Guid>();
       productId.Should().NotBeEmpty();
       
       // Assert - database
       var dbContext = Scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
       var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == new ProductId(productId));
       
       product.Should().NotBeNull();
       product.Name.Should().Be("iPhone 15");
       product.Price.Amount.Should().Be(999.99m);
       product.Price.Currency.Should().Be("EUR");
       product.IsActive.Should().BeTrue();
   }

   [Fact]
   public async Task CreateProduct_WhenNameIsEmpty_ShouldReturn400BadRequest()
   {
      // Arrange
      var request = new
      {
         name = "",
         price = 999.99m,
         currency = "EUR",
      };
      
      // Act
       var response = await Client.PostAsJsonAsync("/products", request);
       
       // Assert
       response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
   }

   [Fact]
   public async Task GetProductById_WhenProductExists_ShouldReturn200WithCorrectData()
   {
      
      // Arrange
      var request = new
      {
         name = "Macbook Pro",
         price = 2499.99m,
         currency = "EUR",
      };
      
      var createResponse = await Client.PostAsJsonAsync("/products", request);
      var productId = await createResponse.Content.ReadFromJsonAsync<Guid>();
       
      // Act
      var getResponse = await Client.GetAsync($"/products/{productId}");
      
      // Assert
      getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

      var body = await getResponse.Content.ReadFromJsonAsync<ProductResponseDto>();
      body.Should().NotBeNull();
      body.Name.Should().Be("Macbook Pro");
      body.Price.Should().Be(2499.99m);
      body.Currency.Should().Be("EUR");
      body.IsActive.Should().BeTrue();
   }
   
   private sealed record ProductResponseDto(
      Guid Id,
      string Name,
      decimal Price,
      string Currency,
      bool IsActive);
}
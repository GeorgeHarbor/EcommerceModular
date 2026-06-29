using Catalog.Domain.Entities;
using Catalog.Domain.Errors;
using Catalog.Domain.Events;
using Catalog.Domain.ValueObjects;
using FluentAssertions;

namespace Unit.Catalog.Domain;

public class ProductTests
{
  private static Money ValidPrice => Money.Create(99.99m, "USD");


  [Fact]
  public void Create_WhenNameAndPriceAreValid_ShouldSucceed()
  {
    // Arrange
    const string name = "iPhone 15";
    var price = ValidPrice;

    //Act
    var result = Product.Create(name, price);

    // Assert
    result.IsSuccess.Should().BeTrue();
    result.Value.Name.Should().Be(name);
    result.Value.Price.Should().Be(price);
    result.Value.IsActive.Should().Be(true);
  }

  [Fact]
  public void Create_WhenNameIsEmpty_ShouldFail()
  {
    // Arrange
    var price = ValidPrice;
    
    // Act
    var result = Product.Create("", price);
    
    // Assert
    result.IsFailure.Should().BeTrue();
    result.Error.Should().Be(ProductErrors.NameEmpty);
  }

  [Fact]
  public void Create_WhenValid_ShouldRaiseProductCreatedEvent()
  {
    // Arrange and act
    var result = Product.Create("iPhone 15", ValidPrice);
    
    // Assert
    result.Value.GetDomainEvents()
      .Should().ContainSingle()
      .Which.Should().BeOfType<ProductCreated>();
  }

  [Fact]
  public void Deactivate_WhenProductIsActive_ShouldSucceed()
  {
    // Arrange
    var product = Product.Create("iPhone 15", ValidPrice);
    
    // Act
    var result = product.Value.Deactivate();
    
    // Assert
    result.IsSuccess.Should().BeTrue();
    product.Value.IsActive.Should().Be(false);
  }

  [Fact]
  public void Deactivate_WhenProductIsNotActive_ShouldFail()
  {
    // Arrange
    var product = Product.Create("iPhone 15", ValidPrice);
    product.Value.Deactivate();
    // Act
    var result = product.Value.Deactivate();
    // Assert
    result.IsFailure.Should().BeTrue();
    result.Error.Should().Be(ProductErrors.AlreadyDeactivated);
  }
  
  [Fact]
  public void UpdatePrice_WhenPriceIsZero_ShouldFail()
  {
    // Arrange
    var product = Product.Create("iPhone 15", ValidPrice).Value;
    var zeroPrice = Money.Create(0.0m, "USD");
    
    // Act
    var result = product.UpdatePrice(zeroPrice);
    
    // Assert
    result.IsFailure.Should().BeTrue();
    result.Error.Should().Be(ProductErrors.InvalidPrice);
  }

  [Fact]
  public void UpdateName_WhenNameIsEmpty_ShouldFail()
  {
    // Arrange
    var product = Product.Create("iPhone 15s", ValidPrice).Value;
    // Act
    var result = product.UpdateName(" ");
    // Assert
    result.IsFailure.Should().BeTrue();
    result.Error.Should().Be(ProductErrors.NameEmpty);
  }
}

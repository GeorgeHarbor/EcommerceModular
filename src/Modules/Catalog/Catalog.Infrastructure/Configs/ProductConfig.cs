using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Configs;

internal sealed class ProductConfig: IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder) 
    {
        builder.ToTable("products");
        
        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => new ProductId(value))
            .ValueGeneratedNever();

        builder.Property(p => p.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.OwnsOne(p => p.Price, priceBuilder =>
        {
            priceBuilder.Property(p => p.Amount)
                .HasColumnName("price_amount")
                .HasColumnType("decimal(18,2)");
            priceBuilder.Property(p => p.Currency)
                .HasColumnName("price_currency")
                .HasMaxLength(3);
        });

        builder.Property(p => p.IsActive)
            .IsRequired();

    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wait.Entities;

namespace Wait.Configurations;


public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.ProductId);

        builder.Property(x => x.ProductId).HasDefaultValue(1).ValueGeneratedOnAdd();
        builder.Property(x => x.ProductName).HasMaxLength(100).IsRequired();
        
    }
}
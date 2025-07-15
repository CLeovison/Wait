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
        builder.Property(x => x.Category).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.Size).IsRequired();
        builder.Property(x => x.Image).IsRequired();
        builder.Property(x => x.IsSoftDelete).HasDefaultValue(false);
        builder.Property(d => d.CreatedAt).HasDefaultValueSql("current_date");
        builder.Property(x => x.ModifiedAt).HasDefaultValueSql("current_date");

        builder.ToTable("Products");
    }
}
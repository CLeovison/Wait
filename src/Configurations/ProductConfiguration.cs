using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wait.Entities;

namespace Wait.Configurations;


public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.ProductId);

        builder.Property(x => x.ProductId)
        .ValueGeneratedOnAdd();

        builder.Property(x => x.ProductName)
        .HasMaxLength(100)
        .IsRequired();

        builder.Property(x => x.Description)
        .IsRequired();

        builder.Property(x => x.Color)
        .IsRequired();
        
        builder.Property(x => x.Price)
        .IsRequired();

        builder.Property(x => x.Quantity)
        .IsRequired();

        builder.Property(x => x.Size)
        .HasMaxLength(50)
        .IsRequired();

        builder.Property(x => x.ImageName)
        .IsRequired();

        builder.Property(d => d.CreatedAt)
        .HasDefaultValueSql("current_date");

        builder.Property(x => x.ModifiedAt)
        .ValueGeneratedOnUpdate()
        .HasDefaultValueSql("current_date");

        builder.HasOne(x => x.Category)
        .WithMany(c => c.Products)
        .HasForeignKey(x => x.CategoryId);
    }
}
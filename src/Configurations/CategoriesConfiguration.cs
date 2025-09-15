using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wait.Domain.Entities;

namespace Wait.Configurations;

public sealed class CategoriesConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasIndex(c => c.CategoryName).IsUnique();

        builder.Property(x => x.CategoryId).ValueGeneratedOnAdd();
        builder.Property(x => x.CategoryName).HasMaxLength(50).IsRequired();
        builder.Property(x => x.CategoryDescription).IsRequired();
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("current_date");
        builder.Property(x => x.ModifiedAt).HasDefaultValueSql("current_date").ValueGeneratedOnUpdate();
        builder.HasMany(c => c.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Cascade);
    }
}
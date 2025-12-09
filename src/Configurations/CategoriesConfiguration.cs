
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wait.Domain.Entities;

namespace Wait.Configurations;

public sealed class CategoriesConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.CategoryId);
        builder.HasIndex(c => c.CategoryName);

        builder.Property(x => x.CategoryId).ValueGeneratedOnAdd();
        builder.Property(x => x.CategoryName).HasMaxLength(50).IsRequired();
        builder.Property(x => x.CategoryDescription).IsRequired();
        builder.Property(x => x.CreatedAt).ValueGeneratedOnAdd().HasDefaultValueSql("current_date");
        builder.Property(x => x.ModifiedAt).ValueGeneratedOnUpdate().HasDefaultValueSql("current_date");
        builder.HasMany(c => c.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Cascade);
        

        
    }
}
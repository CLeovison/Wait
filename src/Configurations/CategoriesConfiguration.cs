using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wait.Domain.Entities;

namespace Wait.Configurations;

public sealed class CategoriesConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.CategoryId);

        builder.Property(x => x.CategoryId).ValueGeneratedOnAdd();
        builder.Property(x => x.CategoryName).HasMaxLength(50).IsRequired();
    }
}
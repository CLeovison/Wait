using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wait.Domain.Entities;

namespace Wait.Configurations;


public sealed class TokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.TokenId);
        builder.HasIndex(x => x.Token).IsUnique();

        builder.Property(x => x.TokenId)
        .HasDefaultValueSql("gen_random_guid()")
        .ValueGeneratedOnAdd();

        builder.Property(x => x.Token)
        .HasMaxLength(200)
        .IsRequired();

        builder.Property(x => x.CreatedAt)
        .HasDefaultValueSql("current_date");
        
        builder.Property(x => x.ExpiresAt)
        .HasDefaultValueSql("current_date");

        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);

    }
}
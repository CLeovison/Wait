using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wait.Domain.Entities;

namespace Wait.Configurations;


public sealed class TokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasIndex(x => x.TokenId);

        builder.Property(x => x.TokenId)
        .HasDefaultValueSql("gen_random_guid()")
        .ValueGeneratedOnAdd();

        builder.Property(x => x.Token).IsRequired();

        builder.Property(x => x.ExpiresOnUtc)
        .HasDefaultValueSql("current_date");

        builder.Property(x => x.RevokedToken).IsRequired();

        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);

    }
}
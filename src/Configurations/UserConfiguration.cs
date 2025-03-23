
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wait.Entities;

namespace Wait.Configurations;
public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(c => c.UserId);
        builder.Property(c => c.UserId).HasDefaultValueSql("gen_random_uuid()").ValueGeneratedOnAdd();
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();

    }
}
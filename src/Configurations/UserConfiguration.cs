
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wait.Contracts.Data;

namespace Wait.Configurations;
public sealed class UserDtoConfiguration : IEntityTypeConfiguration<UserDto>
{
    public void Configure(EntityTypeBuilder<UserDto> builder)
    {
        builder.HasKey(c => c.UserId);
        builder.Property(c => c.UserId);
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.Username).IsRequired();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(d => d.CreatedAt).HasDefaultValueSql("getdate()");
    }
}
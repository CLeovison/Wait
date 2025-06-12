using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wait.Domain.Entities;

namespace Wait.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<Users>
{
    public void Configure(EntityTypeBuilder<Users> builder)
    {

        builder.HasKey(c => c.UserId);

        builder.Property(c => c.UserId)
            .HasDefaultValueSql("gen_random_uuid()")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(x => x.Email)
            .IsRequired();

        builder.Property(d => d.CreatedAt)
            .HasDefaultValueSql("current_date");

        builder.Property(d => d.ModifiedAt)
            .HasDefaultValueSql("current_date");

        builder.Property(b => b.Birthday)
        .HasColumnType("date")
        .IsRequired();


        builder.ToTable("Users");
    }
}
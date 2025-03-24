using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wait.Entities;

namespace Wait.Configurations;
public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Set the primary key
        builder.HasKey(c => c.UserId);

        // Add an index for UserId
        builder.HasIndex(c => c.UserId);

        // Configure UserId to use a default UUID generator (PostgreSQL-specific)
        builder.Property(c => c.UserId)
            .HasDefaultValueSql("gen_random_uuid()")
            .ValueGeneratedOnAdd();

        // Configure FirstName and LastName as required with length constraints
        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100); // Adjust length as needed

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100); // Adjust length as needed

        // Optionally, specify the table name explicitly
        builder.ToTable("Users");
    }
}
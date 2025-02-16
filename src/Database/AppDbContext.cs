using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wait.Contracts.Data;

namespace Wait.Database;

public sealed class AppDbContext : DbContext
{

    /// <summary>
    /// The AppDbContext Constructor takes a paramter of DbContextOptions and passes to the
    /// base class constructor. This paramter is use to configure the context, such as specifying
    /// the database provider and connection string.
    /// 
    /// The Method Database.EnsureCreated is being called to ensure that the database is 
    //  created if it doesn't already exist
    // 
    // The Database.EnsureCreated method must be use only during development and not in production
    /// </summary>
    /// <param name="options"></param>
    public AppDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    public required DbSet<UserDto> UserDto { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserDtoConfiguration());
    }

    public sealed class UserDtoConfiguration : IEntityTypeConfiguration<UserDto>{
        public void Configure(EntityTypeBuilder<UserDto> builder){
            builder.HasKey(c => c.UserId);

        }
    }

}
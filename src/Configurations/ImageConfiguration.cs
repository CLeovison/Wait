using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wait.Infrastructure.Common;

namespace Wait.Configurations;

public sealed class ImageConfiguration : IEntityTypeConfiguration<ImageResult>
{
  public void Configure(EntityTypeBuilder<ImageResult> builder)
  {
    builder.HasKey(x => x.ObjectKey);

    builder.Property(x => x.ObjectKey).IsRequired();
    builder.Property(x => x.StorageUrl).IsRequired();
    builder.Property(x => x.MimeType).IsRequired();
    builder.Property(x => x.FileExtension).IsRequired();
    builder.Property(x => x.OriginalFileName).IsRequired();
    builder.Property(x => x.FileLength);
    builder.Property(x => x.DateUploaded).ValueGeneratedOnAdd().HasDefaultValueSql("current_date");
    builder.Property(x => x.DateModified).ValueGeneratedOnUpdate().HasDefaultValueSql("current_date");
    builder.Property(x => x.UserId).IsRequired();
    builder.HasOne(x => x.Product).WithMany(x => x.ImageUrl).IsRequired();
  }
}
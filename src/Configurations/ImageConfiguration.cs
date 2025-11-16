using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wait.Infrastructure.Common;

namespace Wait.Configurations;

public sealed class ImageConfiguration : IEntityTypeConfiguration<ImageResult>
{
  public void Configure(EntityTypeBuilder<ImageResult> builder)
  {
    builder.HasKey(x => x.ImageId);

    builder.Property(x => x.ImageName).IsRequired();
    builder.Property(x => x.ImagePath).IsRequired();
    builder.Property(x => x.Url).IsRequired();
    builder.Property(x => x.UploadedAt).IsRequired();
  }
}
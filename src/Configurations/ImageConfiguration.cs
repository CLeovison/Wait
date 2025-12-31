using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wait.Infrastructure.Common;

namespace Wait.Configurations;

public sealed class ImageConfiguration : IEntityTypeConfiguration<ImageResult>
{
       public void Configure(EntityTypeBuilder<ImageResult> builder)
       {
              builder.HasKey(x => x.ImageId);

              builder.Property(x => x.ImageId)
                     .ValueGeneratedNever();

              builder.Property(x => x.ObjectKey)
                     .IsRequired()
                     .HasMaxLength(512);

              builder.Property(x => x.StorageUrl)
                     .IsRequired()
                     .HasMaxLength(2048);

              builder.Property(x => x.MimeType)
                     .IsRequired()
                     .HasMaxLength(100);

              builder.Property(x => x.FileExtension)
                     .IsRequired()
                     .HasMaxLength(20);

              builder.Property(x => x.OriginalFileName)
                     .IsRequired()
                     .HasMaxLength(255);

              builder.Property(x => x.FileLength)
                     .IsRequired();

              builder.Property(x => x.DateUploaded)
                     .HasDefaultValueSql("now()")
                     .ValueGeneratedOnAdd();

              builder.Property(x => x.DateModified)
                     .HasDefaultValueSql("now()")
                     .ValueGeneratedOnAddOrUpdate();

              builder.HasIndex(x => x.ObjectKey).IsUnique();
              builder.HasIndex(x => x.ProductId);
              builder.HasIndex(x => x.UserId);

              builder.HasOne(x => x.Product)
                     .WithMany(p => p.ImageUrl)
                     .HasForeignKey(x => x.ProductId);
     

  }
}

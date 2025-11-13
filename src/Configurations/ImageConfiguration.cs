using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wait.Domain.Entities;
using Wait.Infrastructure.Common;

namespace Wait.Configurations;

public sealed class ImageConfiguration : IEntityTypeConfiguration<ImageResult>
{
    public void Configure(EntityTypeBuilder<ImageResult> builder)
    {

    }
}
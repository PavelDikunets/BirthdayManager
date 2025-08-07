using BirthdayManager.Domain.Photos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BirthdayManager.Infrastructure.DataAccess.Configurations;

public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.ToTable("Photos");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Content).IsRequired().HasMaxLength(10_485_760);
        builder.Property(x => x.ContentType).IsRequired();


        builder.HasOne(x => x.Contact)
            .WithOne(x => x.Photo)
            .HasForeignKey<Photo>(x => x.ContactId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
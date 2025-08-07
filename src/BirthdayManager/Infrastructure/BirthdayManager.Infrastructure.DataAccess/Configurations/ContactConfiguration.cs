using BirthdayManager.Domain.Contacts;
using BirthdayManager.Domain.Photos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BirthdayManager.Infrastructure.DataAccess.Configurations;

/// <summary>
/// Конфигурация сущности контакта.
/// </summary>
public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    /// <summary>
    /// Настраивает сущность контакта в таблицу базы данных.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contacts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Birthday).IsRequired();
        builder.Property(x => x.Type).IsRequired();


        builder.HasOne(x => x.Photo)
            .WithOne(x => x.Contact)
            .HasForeignKey<Photo>(x => x.ContactId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.AdvertisementApp.Entities;

namespace Project.AdvertisementApp.DataAccess.EntityConfigurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(300).IsRequired();
            builder.Property(x => x.SurName).HasMaxLength(300).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(300).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(50).IsRequired();
            builder.HasOne(x => x.Gender).WithMany(x => x.AppUsers).HasForeignKey(x => x.GenderId);
        }
    }
}

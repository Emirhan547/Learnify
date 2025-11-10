using Learnify.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learnify.DataAccess.Configurations
{
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.FullName)
                   .HasMaxLength(100);

            builder.Property(x => x.Profession)
                   .HasMaxLength(100);

            builder.Property(x => x.ProfileImage)
                   .HasMaxLength(300)
                   .IsRequired(false);

            // Index for login & search ops
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.UserName).IsUnique();
        }
    }
}

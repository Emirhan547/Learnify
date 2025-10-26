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

            builder.Property(x => x.IsActive)
                   .HasDefaultValue(true);

            builder.Property(x => x.CreatedDate)
                   .HasDefaultValueSql("GETDATE()");
        }
    }
}

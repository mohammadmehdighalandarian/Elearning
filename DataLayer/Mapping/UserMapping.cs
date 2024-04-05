using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserName).IsRequired().HasMaxLength(200);

            builder.Property(x => x.Password).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Email).IsRequired();

            builder.Property(x => x.ActivateCode).IsRequired().HasMaxLength(100);

           

            #region Relation

            builder.HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.Wallets)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UsersId);

            #endregion


        }
    }
}

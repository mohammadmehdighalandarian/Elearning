using DataLayer.Entities.Wallet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Mapping
{
    public class WalletMapping: IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {


            builder.HasOne(x => x.User)
                .WithMany(x => x.Wallets)
                .HasForeignKey(x => x.UsersId);

            builder.HasOne(x => x.WalletType)
                .WithMany(x => x.Wallets)
                .HasForeignKey(x => x.TypesId);
        }
    }
}

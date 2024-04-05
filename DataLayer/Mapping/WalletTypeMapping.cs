using DataLayer.Entities.Wallet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Mapping
{
    public class WalletTypeMapping:IEntityTypeConfiguration<WalletType>
    {
        public void Configure(EntityTypeBuilder<WalletType> builder)
        {


            builder.HasMany(x => x.Wallets)
                .WithOne(x => x.WalletType)
                .HasForeignKey(x => x.TypesId);
        }
    }
}

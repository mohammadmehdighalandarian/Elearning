using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Wallet
{
    public class WalletType
    {
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TypeId { get; set; }

        [Required]
        [MaxLength(150)]
        public string TypeTitle { get; set; }



        #region Relation

        public  List<Wallet> Wallets { get; set; }

        #endregion
    }
}

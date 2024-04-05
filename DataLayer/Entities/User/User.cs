using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.User
{
    public class User
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string ActivateCode { get; set; }

        public bool IsActive { get; set; } = false;

        public string UserAvatar { get; set; }

        public DateTime RegisterDate { get; set; }

        public bool Isdeleted { get; set; }=false;

        public List<UserRole> UserRoles { get; set; }

       
        #region Relation

        
        public  List<Wallet.Wallet> Wallets { get; set; }

        #endregion

    }
}

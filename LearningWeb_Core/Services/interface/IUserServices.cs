using DataLayer.Entities.User;
using DataLayer.Entities.Wallet;
using LearningWeb_Core.DTOs.Account;
using LearningWeb_Core.DTOs.AdminPanel;
using LearningWeb_Core.DTOs.UserPanel;
using LearningWeb_Core.DTOs.UserPanel.Wallet;

namespace LearningWeb_Core.Services
{
    public interface IUserServices
    {
        bool IsEmailExist(string email);

        bool IsUserNameExist(string userName);

        long AddUser(User user);

        void UpdateUser(User user);

        void SaveChange();

        User GetUserBy(string username);

        User GetUserBy(long id);

        User getUserByActiveCode(string activeCode);

        User loginUser(LoginViewModel user);

        User GetUserByEmail(string email);

        bool ActiveUser(string activationCode);

        void ResetPassword(string activeCode,string newPassword);

        long GetUserIdByUserName(string userName);

        #region User Panel

        PannelAccountViewModel GetInformaion(string userName);
        //PannelAccountViewModel GetUserInformation(int userId);

        EditUserViewModel EditUser(string username);

        EditPictureUser editPicture(string username);

        void EditPicture(string username, EditPictureUser editPicture);
        void EditProfile(string username,EditUserViewModel editUser);
        SideBarUserPanelViewModel sideBarInfo(string username);

        bool IsOldPassTrue(string username,string oldPass);
        void ChangePassword(string username,string newPassword);


        #endregion


        #region Wallet

        int BalanceUserWallet(string username);
        List<WalletViewModel>GetAllTransactions(string username);
        long ChargeWallet(string userName, int amount, string description, bool isPay = false);
        long AddWallet(Wallet wallet);
        Wallet GetWalletByWalletId(long walletId);
        void UpdateWallet(Wallet wallet);

        #endregion


        #region Admin Panel  

        UserForAdminViewModel GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");
        long CreateUserByAdmin(CreateUserForAdminViewmodel model);
        EditUserByAdminViewModel ShowUserForEditbyAdmin(long id);
        void EditUserByAmin(long id, EditUserByAdminViewModel model);
        PannelAccountViewModel ShowInfoForDelete(long id);

        void DeleteUser(long id);
        UserForAdminViewModel GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");
        void ReAlloce(long id);

        #endregion



    }
}

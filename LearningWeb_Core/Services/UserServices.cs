using DataLayer.Content;
using DataLayer.Entities.User;
using DataLayer.Entities.Wallet;
using LearningWeb_Core.DTOs.Account;
using LearningWeb_Core.DTOs.AdminPanel;
using LearningWeb_Core.DTOs.UserPanel;
using LearningWeb_Core.DTOs.UserPanel.Wallet;
using LearningWeb_Core.Generator;
using Microsoft.EntityFrameworkCore;


namespace LearningWeb_Core.Services
{
    public class UserServices : IUserServices
    {
        private readonly SiteContext _siteContext;

        public UserServices(SiteContext siteContext)
        {
            _siteContext = siteContext;
        }

        public bool IsEmailExist(string email)
        {
            return _siteContext.Users.Any(u => u.Email == email);
        }

        public bool IsUserNameExist(string userName)
        {
            return _siteContext.Users.Any(x => x.UserName == userName);
        }

        public void UpdateUser(User user)
        {
            _siteContext.Users.Update(user);
        }

        public void SaveChange()
        {
            _siteContext.SaveChanges();
        }

        public User GetUserBy(string username)
        {
            return _siteContext.Users.SingleOrDefault(x => x.UserName == username);
        }

        public User GetUserBy(long id)
        {
            return _siteContext.Users.Find(id);
        }

        public User getUserByActiveCode(string activeCode)
        {
            return _siteContext.Users.SingleOrDefault(x => x.ActivateCode == activeCode);
        }

        public long AddUser(User user)
        {
            _siteContext.Users.Add(user);
            SaveChange();
            return user.Id;
        }

        public User loginUser(LoginViewModel user)
        {
            return _siteContext.Users.SingleOrDefault(x => x.Email == user.Email);
        }

        public User GetUserByEmail(string email)
        {
            return _siteContext.Users.SingleOrDefault(x => x.Email == email);
        }

        public bool ActiveUser(string activationCode)
        {
            var User = _siteContext.Users.SingleOrDefault(x => x.ActivateCode == activationCode);

            if (User == null || User.IsActive)
            {
                return false;
            }
            User.IsActive = true;
            User.ActivateCode = UniqCode.GenerateUniqCode();
            SaveChange();

            return true;
        }

        public void ResetPassword(string activeCode, string newPassword)
        {
            var User = getUserByActiveCode(activeCode);
            User.Password = newPassword;
            _siteContext.Users.Update(User);
            SaveChange();

        }

        public long GetUserIdByUserName(string userName)
        {
            return _siteContext.Users.Single(x => x.UserName == userName).Id;
        }

        public PannelAccountViewModel GetInformaion(string username)
        {
            var User = GetUserBy(username);
            PannelAccountViewModel UserVM = new PannelAccountViewModel()
            {
                UserName = User.UserName,
                Email = User.Email,
                RegisterDate = User.RegisterDate,
                Wallet = BalanceUserWallet(username)
            };
            return UserVM;
        }


        public EditUserViewModel EditUser(string username)
        {
            return _siteContext.Users.Where(x => x.UserName == username).Select(x => new EditUserViewModel()
            {
                userName = x.UserName,
                Email = x.Email,
            }).Single();
        }

        public EditPictureUser editPicture(string username)
        {
            return _siteContext.Users.Where(x => x.UserName == username).Select(u => new EditPictureUser()
            {
                AvatarName = u.UserAvatar
            }).Single();
        }

        public void EditPicture(string username, EditPictureUser editPicture)
        {
            if (editPicture.UserAvatar != null)
            {
                string imagePath = "";
                if (editPicture.AvatarName != "Defualt.png")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", editPicture.AvatarName);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }

                editPicture.AvatarName = UniqCode.GenerateUniqCode() + Path.GetExtension(editPicture.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", editPicture.AvatarName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    editPicture.UserAvatar.CopyTo(stream);
                }
            }

            var user = GetUserBy(username);

            user.UserAvatar = editPicture.AvatarName;


            UpdateUser(user);
            SaveChange();
        }

        public SideBarUserPanelViewModel sideBarInfo(string username)
        {
            return _siteContext.Users.Where(x => x.UserName == username).Select(u => new SideBarUserPanelViewModel()
            {
                UserName = u.UserName,
                ImageName = u.UserAvatar,
                RegisterDate = u.RegisterDate
            }).Single();
        }

        public bool IsOldPassTrue(string username, string oldPass)
        {
            return _siteContext.Users.Any(x => x.UserName == username && x.Password == oldPass);
        }

        public void ChangePassword(string username, string newPassword)
        {
            var user = GetUserBy(username);
            user.Password = newPassword;
            UpdateUser(user);
            SaveChange();
        }

        public int BalanceUserWallet(string username)
        {
            long CurentUserId = GetUserIdByUserName(username);

            var variz = _siteContext.Wallets
                .Where(x => x.UsersId == CurentUserId && x.IsPay == true && x.TypesId == 1)
                .Select(x => x.Amount)
                .ToList();

            var Bardasht = _siteContext.Wallets
                .Where(x => x.UsersId == CurentUserId && x.IsPay == true && x.TypesId == 2)
                .Select(x => x.Amount)
                .ToList();

            return variz.Sum() - Bardasht.Sum();
        }

        public List<WalletViewModel> GetAllTransactions(string username)
        {
            long CurentUserId = GetUserIdByUserName(username);
            return _siteContext.Wallets.Where(x => x.UsersId == CurentUserId)
                .Select(x => new WalletViewModel()
                {
                    Amount = x.Amount,
                    DateTime = x.CreateDate,
                    Description = x.Description,
                    Type = x.TypesId,
                    Status = x.IsPay.ToString()
                }).ToList();
        }

        public long ChargeWallet(string userName, int amount, string description, bool isPay = false)
        {
            Wallet wallet = new Wallet()
            {
                Amount = amount,
                CreateDate = DateTime.Now,
                Description = description,
                IsPay = isPay,
                TypesId = 1,
                UsersId = GetUserIdByUserName(userName)
            };
            return AddWallet(wallet);
        }

        public long AddWallet(Wallet wallet)
        {
            _siteContext.Wallets.Add(wallet);
            SaveChange();
            return wallet.WalletId;
        }

        public Wallet GetWalletByWalletId(long walletId)
        {
            return _siteContext.Wallets.Find(walletId);
        }

        public void UpdateWallet(Wallet wallet)
        {
            _siteContext.Wallets.Update(wallet);
            SaveChange();
        }

        public UserForAdminViewModel GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _siteContext.Users;

            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email.Contains(filterEmail) );
            }

            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => u.UserName.Contains(filterUserName) );
            }
            // Show Item In Page
            int take = 20;
            int skip = (pageId - 1) * take;


            UserForAdminViewModel list = new UserForAdminViewModel();
            list.CurrentPage = pageId;
            list.PageCount = result.Count() / take;
            list.Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public long CreateUserByAdmin(CreateUserForAdminViewmodel model)
        {
            string imagePath = model.PathImage;

            if (model.UserAvatar != null)
            {

                imagePath = UniqCode.GenerateUniqCode() + Path.GetExtension(model.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", imagePath);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    model.UserAvatar.CopyTo(stream);
                }
            }

            User user = new User()
            {
                UserAvatar = imagePath,
                ActivateCode = UniqCode.GenerateUniqCode(),
                Email = model.Email,
                IsActive = true,
                Password = model.Password,
                RegisterDate = DateTime.Now,
                UserName = model.UserName,
                Isdeleted = false

            };
            return AddUser(user);
        }

        public EditUserByAdminViewModel ShowUserForEditbyAdmin(long id)
        {
            return _siteContext
                .Users
                .Where(x => x.Id == id)
                .Select(x => new EditUserByAdminViewModel
                {
                    UserName = x.UserName,
                    Email = x.Email,
                    PathImage = x.UserAvatar,
                    Roles = _siteContext.UserRoles.Where(x => x.UserId == id).Select(x => x.RoleId).ToList()
                }).Single();
        }

        public void EditUserByAmin(long id, EditUserByAdminViewModel model)
        {
            User user = GetUserBy(id);
            user.Email = model.Email;
            user.Password = model.Password;

            string imagePath = model.PathImage;
            if (model.UserAvatar != null)
            {

                if (imagePath != "Defualt.png")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", imagePath);
                    File.Delete(imagePath);
                }

                imagePath = UniqCode.GenerateUniqCode() + Path.GetExtension(model.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", imagePath);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    model.UserAvatar.CopyTo(stream);
                }

            }

            user.UserAvatar = imagePath;

            UpdateUser(user);
            SaveChange();
        }

        public PannelAccountViewModel ShowInfoForDelete(long id)
        {
           return _siteContext.Users.Where(x => x.Id == id)
                .Select(x => new PannelAccountViewModel
                {
                    UserName = x.UserName,
                    Email = x.Email,
                    RegisterDate = x.RegisterDate,
                    Wallet = x.Wallets.Where(x=>x.UsersId==id).Select(x=>x.Amount).Sum()
                }).Single();
        }

        public void DeleteUser(long id)
        {
            var user = GetUserBy(id);
            user.IsActive = false;
            user.Isdeleted = true;
            SaveChange();
        }

        public UserForAdminViewModel GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _siteContext.Users.IgnoreQueryFilters().Where(u => u.Isdeleted);

            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email.Contains(filterEmail));
            }

            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => u.UserName.Contains(filterUserName));
            }

            // Show Item In Page
            int take = 20;
            int skip = (pageId - 1) * take;


            UserForAdminViewModel list = new UserForAdminViewModel();
            list.CurrentPage = pageId;
            list.PageCount = result.Count() / take;
            list.Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public void ReAlloce(long id)
        {
            GetUserBy(id).Isdeleted = false;
            SaveChange();
        }


        public void EditProfile(string username, EditUserViewModel editUser)
        {

            var user = GetUserBy(username);
            user.UserName = editUser.userName;
            user.Email = editUser.Email;

            UpdateUser(user);
            SaveChange();
        }
    }
}

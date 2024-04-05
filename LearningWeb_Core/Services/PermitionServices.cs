using System.Security.Cryptography.X509Certificates;
using DataLayer.Content;
using DataLayer.Entities.Permision;
using DataLayer.Entities.User;

namespace LearningWeb_Core.Services
{
    public class PermitionServices : IPermitionServices
    {
        private readonly SiteContext _siteContext;

        public PermitionServices(SiteContext siteContext)
        {
            _siteContext = siteContext;
        }

        #region Role

        public List<Role> GetAllRoles()
        {
            return _siteContext.Roles.ToList();
        }

        public void AddRoleToUser(List<long> role, long userId)
        {
            foreach (var roleid in role)
            {
                _siteContext.UserRoles.Add(new UserRole()
                {
                    RoleId = roleid,
                    UserId = userId
                });

            }

            _siteContext.SaveChanges();
        }

        public void UpdateRolesOfUser(List<long> role, long userId)
        {
            var userRolesOfUser = _siteContext.UserRoles.Where(x => x.UserId == userId);
            _siteContext.UserRoles.RemoveRange(userRolesOfUser);

            AddRoleToUser(role, userId);

        }

        public Role GetRole(long roleId)
        {
            return _siteContext.Roles.Find(roleId);
        }

        public void UpdateRole(Role role)
        {
            _siteContext.Roles.Update(role);
            SaveChange();
        }

        public void DeleteRole(Role role)
        {
            role.IsDeleted = true;
            UpdateRole(role);
        }

        public long CreateRole(Role role)
        {
            _siteContext.Roles.Add(role);
            SaveChange();
            return role.Id;
        }

        public void SaveChange()
        {
            _siteContext.SaveChanges();
        }

        #endregion

        #region Permition

        public List<Permision> GetAllPermitions()
        {
            return _siteContext.Permisions.ToList();
        }

        public List<long> GetPermitionOfRole(long roleId)
        {
            return _siteContext.RolePermisions
                .Where(x => x.RoleId == roleId)
                .Select(x => x.PermissionId)
                .ToList();
        }

        public void AddPermitionToRole(long roleId, List<long> permitions)
        {
            foreach (var permition in permitions)
            {
                _siteContext.RolePermisions.Add(new RolePermision
                {
                    RoleId = roleId,
                    PermissionId = permition,
                });

            }
            SaveChange();
        }

        public void UpdatePermitionOfRole(List<long> permition, long roleId)
        {
            var Remove = _siteContext.RolePermisions.Where(x => x.RoleId == roleId);
            _siteContext.RolePermisions.RemoveRange(Remove);

            foreach (var p in permition)
            {
                _siteContext.RolePermisions.Add(new RolePermision
                {
                    RoleId = roleId,
                    PermissionId = p
                });
            }

            SaveChange();
        }

        public bool CheckPermission(long permisionId, string username)
        {
            long userId = _siteContext.Users.Single(x=>x.UserName==username).Id;

            List<long> RoleOfUser =
                _siteContext
                    .UserRoles
                    .Where(x => x.UserId == userId)
                    .Select(x => x.RoleId)
                    .ToList();

            if (!RoleOfUser.Any())
                return false;


            List<long> PermissionOfRole =
                    _siteContext
                        .RolePermisions
                        .Where(x => x.PermissionId == permisionId)
                        .Select(x => x.RoleId)
                        .ToList();


            return PermissionOfRole.Any(x => RoleOfUser.Contains(x));
            
        }

        #endregion

    }
}

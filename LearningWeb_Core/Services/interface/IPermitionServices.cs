using DataLayer.Entities.Permision;
using DataLayer.Entities.User;

namespace LearningWeb_Core.Services
{
    public interface IPermitionServices
    {
        #region Roles

        List<Role> GetAllRoles();
        void AddRoleToUser(List<long> role, long userId);
        void UpdateRolesOfUser(List<long> role, long userId);
        Role GetRole(long roleId);
        void UpdateRole(Role role);
        void DeleteRole(Role role);
        long CreateRole(Role role);
        void SaveChange();

        #endregion

        #region Permition

        List<Permision> GetAllPermitions();

        List<long>GetPermitionOfRole(long roleId);

        void AddPermitionToRole(long roleId, List<long> permitions);

        void UpdatePermitionOfRole(List<long> permition, long roleId);

        bool CheckPermission(long permisionId,string username);

        #endregion
    }
}

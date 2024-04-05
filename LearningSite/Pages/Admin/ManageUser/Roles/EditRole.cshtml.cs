using DataLayer.Entities.User;
using LearningWeb_Core.Security;
using LearningWeb_Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningSite.Pages.Admin.ManageUser.Roles
{
    [PermissionChecker(9)]
    public class EditRoleModel : PageModel
    {
        private readonly IPermitionServices _permitionServices;

        public EditRoleModel(IPermitionServices permitionServices)
        {
            _permitionServices = permitionServices;
        }

        [BindProperty]
        public Role Role { get; set; }

        public void OnGet(long id)
        {
            Role = _permitionServices.GetRole(id);
            ViewData["Permissions"] = _permitionServices.GetAllPermitions();
            ViewData["SelectedPermissions"] = _permitionServices.GetPermitionOfRole(id);
        }

        public IActionResult OnPost(string Title,List<long> SelectedPermission)
        {
            //if (!ModelState.IsValid)
            //    return Page();
            Role.RoleName = Title;
            _permitionServices.UpdateRole(Role);
            _permitionServices.UpdatePermitionOfRole(SelectedPermission,Role.Id);

            return RedirectToPage("./index");
        }
    }
}

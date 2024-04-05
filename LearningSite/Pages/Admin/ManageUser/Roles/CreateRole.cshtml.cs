using DataLayer.Entities.Permision;
using DataLayer.Entities.User;
using LearningWeb_Core.Security;
using LearningWeb_Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningSite.Pages.Admin.ManageUser.Roles
{
    [PermissionChecker(7)]
    public class CreateRoleModel : PageModel
    {
        private readonly IPermitionServices _permitionServices;

        public CreateRoleModel(IPermitionServices permitionServices)
        {
            _permitionServices = permitionServices;
        }
        [BindProperty]
        public Role Role { get; set; }


        public void OnGet()
        {
            ViewData["Permissions"] = _permitionServices.GetAllPermitions();
        }

        public IActionResult OnPost(List<long> SelectedPermission)
        {
            var Role_id = _permitionServices.CreateRole(Role);
            _permitionServices.AddPermitionToRole(Role_id, SelectedPermission);

            return RedirectToPage("./index");
        }
    }
}

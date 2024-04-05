using DataLayer.Entities.User;
using LearningWeb_Core.Security;
using LearningWeb_Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningSite.Pages.Admin.ManageUser.Roles
{
    [PermissionChecker(8)]
    public class DeleteRoleModel : PageModel
    {
        private readonly IPermitionServices _permitionServices;

        public DeleteRoleModel(IPermitionServices permitionServices)
        {
            _permitionServices = permitionServices;
        }

        [BindProperty]
        public Role Role { get; set; }

        public void OnGet(long id)
        {
            Role = _permitionServices.GetRole(id);
        }

        public IActionResult OnPost()
        {
            _permitionServices.DeleteRole(Role);
            return RedirectToPage("./index");
        }
    }
}

using System.Runtime.CompilerServices;
using LearningWeb_Core.DTOs.AdminPanel;
using LearningWeb_Core.Security;
using LearningWeb_Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningSite.Pages.Admin.ManageUser
{
    [PermissionChecker(4)]
    public class EditUserModel : PageModel
    {
        private readonly IUserServices _userServices;
        private readonly IPermitionServices _permitionServices;

        public EditUserModel(IUserServices userServices, IPermitionServices permitionServices)
        {
            _userServices = userServices;
            _permitionServices = permitionServices;
        }

        [BindProperty]
        public EditUserByAdminViewModel  EditUserByAdmin { get; set; }

        public void OnGet(long id)
        {
            EditUserByAdmin = _userServices.ShowUserForEditbyAdmin(id);
            ViewData["Roles"] = _permitionServices.GetAllRoles();
        }

        public IActionResult OnPost(long id,List<long> selectedRoles)
        {
            //if (!ModelState.IsValid)
            //    return Page();

            _userServices.EditUserByAmin(id,EditUserByAdmin);
            _permitionServices.UpdateRolesOfUser(selectedRoles,id);

            return RedirectToPage("/index");
        }
    }
}

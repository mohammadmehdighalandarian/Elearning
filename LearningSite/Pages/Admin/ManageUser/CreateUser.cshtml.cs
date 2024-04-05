using DataLayer.Entities.User;
using LearningWeb_Core.DTOs.AdminPanel;
using LearningWeb_Core.Security;
using LearningWeb_Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningSite.Pages.Admin.ManageUser
{
    [PermissionChecker(3)]
    public class CreateUserModel : PageModel
    {
        private readonly IUserServices _userServices;
        private readonly IPermitionServices _permitionServices;

        public CreateUserModel(IUserServices userServices, IPermitionServices permitionServices)
        {
            _userServices = userServices;
            _permitionServices = permitionServices;
        }
        [ViewData]
        public List<Role> Roles { get; set; }

        [BindProperty]
        public CreateUserForAdminViewmodel CreateUserForAdminViewmodel { get; set; }

        public void OnGet()
        {
            Roles = _permitionServices.GetAllRoles();
        }

        public IActionResult OnPost(List<long> selectedRoles)
        {
            
            //if (!ModelState.IsValid)
            //    return Page();

            long userId = _userServices.CreateUserByAdmin(CreateUserForAdminViewmodel);

            //Add Roles
            _permitionServices.AddRoleToUser(selectedRoles,userId);


            return Redirect("/Admin/ManageUser");

        }
    }
}

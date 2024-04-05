using LearningWeb_Core.DTOs.UserPanel;
using LearningWeb_Core.Security;
using LearningWeb_Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningSite.Pages.Admin.ManageUser
{
    [PermissionChecker(5)]
    public class DeleteUserModel : PageModel
    {
        private readonly IUserServices _userServices;

        public DeleteUserModel(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public PannelAccountViewModel PannelAccountViewModel { get; set; }
        public void OnGet(long id)
        {
            ViewData["UserId"]=id;
            PannelAccountViewModel = _userServices.ShowInfoForDelete(id);
        }

        public IActionResult OnPost(long id)
        {
            if (!ModelState.IsValid)
                return Page();

            _userServices.DeleteUser(id);
            return RedirectToPage("./index");
        }
    }
}

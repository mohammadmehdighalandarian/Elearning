using LearningWeb_Core.DTOs.AdminPanel;
using LearningWeb_Core.Security;
using LearningWeb_Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningSite.Pages.Admin.ManageUser
{
    [PermissionChecker(1)]
    public class ListOfDeleteUserModel : PageModel
    {
        
        private readonly IUserServices _userServices;

        public ListOfDeleteUserModel(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public UserForAdminViewModel UserForAdmin { get; set; }
        public void OnGet(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            UserForAdmin = _userServices.GetDeleteUsers(pageId, filterEmail, filterUserName);
        }

        
        public IActionResult OnPost(long id)
        {
            _userServices.ReAlloce(id);
            return RedirectToPage("./ListOfDeleteUser");
        }
    }
}

using DataLayer.Entities.User;
using LearningWeb_Core.Security;
using LearningWeb_Core.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningSite.Pages.Admin.ManageUser.Roles
{
    [PermissionChecker(6)]
    public class IndexModel : PageModel
    {
        private readonly IPermitionServices _permitionServices;

        public IndexModel(IPermitionServices permitionServices)
        {
            _permitionServices = permitionServices;
        }

        public List<Role> Roles { get; set; }

        public void OnGet()
        {
            Roles = _permitionServices.GetAllRoles();
        }
    }
}

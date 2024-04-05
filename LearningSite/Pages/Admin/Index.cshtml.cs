using LearningWeb_Core.Security;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningSite.Pages.Admin
{
    [PermissionChecker(1)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

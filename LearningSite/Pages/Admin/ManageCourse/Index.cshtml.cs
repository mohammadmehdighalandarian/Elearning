using DataLayer.Entities.Course;
using LearningWeb_Core.DTOs.AdminPanel;
using LearningWeb_Core.Security;
using LearningWeb_Core.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningSite.Pages.Admin.ManageCourse
{
    [PermissionChecker(2)]
    public class IndexModel : PageModel
    {
        private readonly ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<ShowCourseForAdminViewModel> ListCourses { get; set; }

        public void OnGet()
        {
            ListCourses = _courseService.GetAllCoursesForAdmin();
        }
    }
}

using DataLayer.Entities.Course;
using LearningWeb_Core.Security;
using LearningWeb_Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearningSite.Pages.Admin.ManageCourse
{
    [PermissionChecker(2)]
    public class CreateCourseModel : PageModel
    {
        private readonly ICourseService _courseService;

        public CreateCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public Course Course { get; set; }

        public void OnGet()
        {
            var groups = _courseService.GetGroupForCreateCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");

            var subGrous = _courseService.GetSubGroupForCreateCourse(int.Parse(groups.First().Value));
            ViewData["SubGroups"] = new SelectList(subGrous, "Value", "Text");

            var teachers = _courseService.GetTeachers();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text");

            var levels = _courseService.GetLevels();
            ViewData["Levels"] = new SelectList(levels, "Value", "Text");

            var statues = _courseService.GetStatues();
            ViewData["Statues"] = new SelectList(statues, "Value", "Text");

        }

        public IActionResult OnPost(IFormFile imgCourseUp, IFormFile demoUp)
        {
            //if (!ModelState.IsValid)
            //    return Page();

            _courseService.AddCourse(Course, imgCourseUp, demoUp);

            return RedirectToPage("Index");
        }
    }
}

using DataLayer.Entities.Course;
using LearningWeb_Core.Security;
using LearningWeb_Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearningSite.Pages.Admin.ManageCourse
{
    [PermissionChecker(2)]
    public class EditCourseModel : PageModel
    {
        private readonly ICourseService _courseService;

        public EditCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public Course Course { get; set; }

        public void OnGet()
        {
        }

        public void OnGet(long CourseId)
        {
            Course = _courseService.GetCourseById(CourseId);

            var groups = _courseService.GetGroupForCreateCourse();

            
            ViewData["Groups"] = new SelectList(groups, "Value", "Text", Course.GroupId);

            List<SelectListItem> subgroups = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب کنید",Value = ""}
            };
            subgroups.AddRange(_courseService.GetSubGroupForCreateCourse(Course.Id));
            string selectedSubGroup = null;
            if (Course.SubGroup != null)
            {
                selectedSubGroup = Course.SubGroup.ToString();
            }
            ViewData["SubGroups"] = new SelectList(subgroups, "Value", "Text", selectedSubGroup);

            var teachers = _courseService.GetTeachers();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text", Course.TeacherId);

            var levels = _courseService.GetLevels();
            ViewData["Levels"] = new SelectList(levels, "Value", "Text", Course.LevelId);

            var statues = _courseService.GetStatues();
            ViewData["Statues"] = new SelectList(statues, "Value", "Text", Course.StatusId);

        }
        public IActionResult OnPost(IFormFile imgCourseUp, IFormFile demoUp)
        {
            if (!ModelState.IsValid)
                return Page();

            _courseService.UpdateCourse(Course, imgCourseUp, demoUp);

            return RedirectToPage("Index");
        }
    }
}

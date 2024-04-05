using DataLayer.Entities.Course;
using LearningWeb_Core.Security;
using LearningWeb_Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningSite.Pages.Admin.ManageCourse
{
    [PermissionChecker(2)]
    public class EditEpisodModel : PageModel
    {
        private readonly ICourseService _courseService;

        public EditEpisodModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }

        public void OnGet(long id)
        {
            CourseEpisode = _courseService.GetEpisodeById(id);
        }

        public IActionResult OnPost(IFormFile fileEpisode)
        {
            if (!ModelState.IsValid)
                return Page();

            if (fileEpisode != null)
            {
                if (_courseService.CheckExistFile(fileEpisode.FileName))
                {
                    ViewData["IsExistFile"] = true;
                    return Page();
                }
            }


            _courseService.EditEpisode(CourseEpisode, fileEpisode);

            return Redirect("/Admin/ManegeCourse/IndexEpisode/" + CourseEpisode.CourseId);
        }
    }
}

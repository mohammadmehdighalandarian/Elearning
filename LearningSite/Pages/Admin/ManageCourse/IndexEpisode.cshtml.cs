using DataLayer.Entities.Course;
using LearningWeb_Core.Security;
using LearningWeb_Core.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningSite.Pages.Admin.ManageCourse
{
    [PermissionChecker(2)]
    public class IndexEpisodeModel : PageModel
    {
        private readonly ICourseService _courseService;

        public IndexEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<CourseEpisode> CourseEpisodes { get; set; }

        public void OnGet(long id)
        {
            ViewData["CourseId"] = id;
            CourseEpisodes = _courseService.GetListEpisodeCorse(id);
        }
    }
}

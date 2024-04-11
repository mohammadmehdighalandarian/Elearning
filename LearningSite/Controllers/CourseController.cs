using LearningWeb_Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearningSite.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public IActionResult Index(int pageId = 1, string filter = "", string getType = "all",
            string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null)
        {
            var groups = _courseService.GetAllGroups();
            ViewBag.Groups = groups;

            ViewData["SelectedGrops"] = selectedGroups;
            
            
            ViewBag.pageId = pageId;
            return View(_courseService.GetAllCourseForIndex(pageId, filter, getType, orderByType, startPrice, endPrice, selectedGroups, 9));
        }
    }
}

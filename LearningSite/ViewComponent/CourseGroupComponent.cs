using LearningWeb_Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace LearningSite.ViewComponent
{
    public class CourseGroupComponent:Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private ICourseService _courseService;

        public CourseGroupComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("CourseGroup", _courseService.GetAllCourse()));
        }
    }
}

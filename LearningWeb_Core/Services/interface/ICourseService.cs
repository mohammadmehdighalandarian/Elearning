using DataLayer.Entities.Course;
using LearningWeb_Core.DTOs.AdminPanel;
using LearningWeb_Core.DTOs.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearningWeb_Core.Services
{
    public interface ICourseService
    {

        #region Group
        List<CourseGroup> GetAllGroups();
        List<CourseGroup> GetAllCourse();
        List<SelectListItem> GetGroupForCreateCourse();
        List<SelectListItem> GetSubGroupForCreateCourse(long groupId);
        List<SelectListItem> GetTeachers();
        List<SelectListItem> GetLevels();
        List<SelectListItem> GetStatues();
        CourseGroup GetById(int groupId);
        void AddGroup(CourseGroup group);
        void UpdateGroup(CourseGroup group);

        #endregion

        #region Course

        List<ShowCourseForAdminViewModel> GetAllCoursesForAdmin();

        long AddCourse(Course course, IFormFile imgCourse, IFormFile courseDemo);

        Course GetCourseById(long courseId);

        void UpdateCourse(Course course, IFormFile imgCourse, IFormFile courseDemo);

       Tuple<List<ShowCourseInIndexViewModel>,int>  GetAllCourseForIndex(int pageId = 1, string filter = "", string getType = "all",
            string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0);

        Course GetCourseWithDetailsForShow(long courseId);

        //List<ShowCourseInIndexViewModel> GetPopularCourse();

        bool IsFree(long courseId);


        #endregion

        #region Episode

        List<CourseEpisode> GetListEpisodeCorse(long courseId);
        bool CheckExistFile(string fileName);
        long AddEpisode(CourseEpisode episode, IFormFile episodeFile);
        CourseEpisode GetEpisodeById(long episodeId);
        void EditEpisode(CourseEpisode episode, IFormFile episodeFile);

        #endregion
    }
}

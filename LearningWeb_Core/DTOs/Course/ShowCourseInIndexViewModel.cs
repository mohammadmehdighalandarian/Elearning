using DataLayer.Entities.Course;

namespace LearningWeb_Core.DTOs.Course
{
    public class ShowCourseInIndexViewModel
    {
        public long CourseId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public long Price { get; set; }
        public TimeSpan TotalTime { get; set; }
        public List<CourseEpisode> CourseEpisodes { get; set; }
    }
}

using DataLayer.Content;
using DataLayer.Entities.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearningWeb_Core.Convertors;
using LearningWeb_Core.DTOs.AdminPanel;
using LearningWeb_Core.Generator;
using LearningWeb_Core.Security;

namespace LearningWeb_Core.Services
{
    public class CourseService:ICourseService
    {
        private readonly SiteContext _siteContext;

        public CourseService(SiteContext siteContext)
        {
            _siteContext = siteContext;
        }

        #region Episod

       

        public List<CourseEpisode> GetListEpisodeCorse(long courseId)
        {
            return _siteContext.CourseEpisodes.Where(e => e.CourseId == courseId).ToList();
        }

        public bool CheckExistFile(string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", fileName);
            return File.Exists(path);
        }

        public long AddEpisode(CourseEpisode episode, IFormFile episodeFile)
        {
            episode.EpisodeFileName = episodeFile.FileName;

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", episode.EpisodeFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                episodeFile.CopyTo(stream);
            }

            _siteContext.CourseEpisodes.Add(episode);
            _siteContext.SaveChanges();
            return episode.Id;
        }

        public CourseEpisode GetEpisodeById(long episodeId)
        {
            return _siteContext.CourseEpisodes.Find(episodeId);
        }

        public void EditEpisode(CourseEpisode episode, IFormFile episodeFile)
        {
            if (episodeFile != null)
            {
                string deleteFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", episode.EpisodeFileName);
                File.Delete(deleteFilePath);

                episode.EpisodeFileName = episodeFile.FileName;
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", episode.EpisodeFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    episodeFile.CopyTo(stream);
                }
            }

            _siteContext.CourseEpisodes.Update(episode);
            _siteContext.SaveChanges();
        }


        #endregion

        #region Group

        public List<CourseGroup> GetAllCourse()
        {
            return _siteContext.CourseGroups.ToList();
        }

        public List<SelectListItem> GetGroupForCreateCourse()
        {
            return _siteContext.CourseGroups.Where(g => g.ParentId == null)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.Id.ToString()
                }).ToList();
        }

        public List<SelectListItem> GetSubGroupForCreateCourse(long groupId)
        {
            return _siteContext.CourseGroups.Where(g => g.ParentId == groupId)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.Id.ToString()
                }).ToList();
        }

        public List<SelectListItem> GetTeachers()
        {
            return _siteContext.UserRoles
                .Where(x => x.RoleId == 6)
                .Include(g => g.User)
                .Select(u => new SelectListItem()
                {
                    Value = u.UserId.ToString(),
                    Text = u.User.UserName
                }).ToList();
        }

        public List<SelectListItem> GetLevels()
        {
            return _siteContext.CourseLevels.Select(l => new SelectListItem()
            {
                Value = l.LevelId.ToString(),
                Text = l.LevelTitle
            }).ToList();
        }

        public List<SelectListItem> GetStatues()
        {
            return _siteContext.CourseStatus.Select(s => new SelectListItem()
            {
                Value = s.StatusId.ToString(),
                Text = s.StatusTitle
            }).ToList();
        }

        public CourseGroup GetById(int groupId)
        {
            return _siteContext.CourseGroups.Find(groupId);
        }

        public void AddGroup(CourseGroup @group)
        {
            throw new NotImplementedException();
        }

        public void UpdateGroup(CourseGroup @group)
        {
            throw new NotImplementedException();
        }



    #endregion

    #region Course

        public bool IsFree(long courseId)
        {
            throw new NotImplementedException();
        }

        public List<ShowCourseForAdminViewModel> GetAllCoursesForAdmin()
        {
            return _siteContext.Courses.Select(c=>new ShowCourseForAdminViewModel()
            {
                CourseId = c.Id,
                Title = c.CourseTitle,
                ImageName = c.CourseImageName,
                EpisodeCount = c.CourseEpisodes.Count
            }).ToList();
        }

        public long AddCourse(Course course, IFormFile imgCourse, IFormFile courseDemo)
        {
            course.CreateDate = DateTime.Now;
            course.CourseImageName = "no-photo.jpg";

            //TODO Check Image
            if (imgCourse != null && imgCourse.IsImage())
            {
                course.CourseImageName = UniqCode.GenerateUniqCode() + Path.GetExtension(imgCourse.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image", course.CourseImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgCourse.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/thumb", course.CourseImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 250);
            }

            if (courseDemo != null)
            {
                course.DemoFileName = UniqCode.GenerateUniqCode() + Path.GetExtension(courseDemo.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/demoes", course.DemoFileName);
                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    courseDemo.CopyTo(stream);
                }
            }

            _siteContext.Add(course);
            _siteContext.SaveChanges();

            return course.Id;
        }

        public Course GetCourseById(long courseId)
        {
            return _siteContext.Courses.Find(courseId);
        }

        public void UpdateCourse(Course course, IFormFile imgCourse, IFormFile courseDemo)
        {
            throw new NotImplementedException();
        }

        public Course GetCourseForShow(long courseId)
        {
            throw new NotImplementedException();
        }



        #endregion
    }
}

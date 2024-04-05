using Microsoft.AspNetCore.Http;

namespace LearningWeb_Core.DTOs.UserPanel
{
    public class EditPictureUser
    {
        public IFormFile UserAvatar { get; set; }

        public string AvatarName { get; set; }
    }
}

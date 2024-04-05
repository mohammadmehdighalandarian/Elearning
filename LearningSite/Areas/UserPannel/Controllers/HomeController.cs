using LearningWeb_Core.DTOs.UserPanel;
using LearningWeb_Core.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningSite.Areas.UserPannel.Controllers
{
    [Area("UserPannel")]
    [Authorize]
    public class HomeController : Controller
    {
        
        private readonly IUserServices _userServices;

        public HomeController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public IActionResult Index()
        {

            return View(_userServices.GetInformaion(User.Identity.Name));
        }

        #region EditPannelInfo

        [Route("/UserPannel/Edit")]
        public IActionResult EditUser()
        {
            var EditModel = _userServices.EditUser(User.Identity.Name);
            return View(EditModel);
        }

        [Route("/UserPannel/Edit")]
        [HttpPost]
        public IActionResult EditUser(EditUserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            _userServices.EditProfile(User.Identity.Name, user);

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return Redirect("/login?EditProfile=true");
        }

        #endregion

        #region ChangePannelPicture

        [Route("/UserPannel/EditPic")]
        public IActionResult EditPic()
        {
            return View(_userServices.editPicture(User.Identity.Name));
        }

        [Route("/UserPannel/EditPic")]
        [HttpPost]
        public IActionResult EditPic(EditPictureUser editePictureUser)
        {
            _userServices.EditPicture(User.Identity.Name, editePictureUser);
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/login?EditProfile=true");
        }

        #endregion

        #region ChangePassword

        [Route("/UserPannel/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("/UserPannel/ChangePassword")]
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel changePassword)
        {
            string CurentUserName = User.Identity.Name;
            if (!ModelState.IsValid)
                return View(changePassword);

            if (!_userServices.IsOldPassTrue(CurentUserName, changePassword.OldPassword))
            {
                ModelState.AddModelError("OldPassword","رمز وارد شده صحیح نمیباشد");
                return View(changePassword);
            }

            if (string.Compare(changePassword.Password,changePassword.RePassword)==1)
            {
                ModelState.AddModelError("RePassword", "رمز عبور با تکرار ان یکسان نیست");
                return View(changePassword);
            }

           

            _userServices.ChangePassword(CurentUserName,changePassword.Password);
            ViewBag.IsSuccess = "true";
            return View();
        }

        #endregion
    }
}

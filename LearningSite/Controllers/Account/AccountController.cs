using System.Security.Claims;
using DataLayer.Entities.User;
using LearningWeb_Core.Convertors;
using LearningWeb_Core.DTOs.Account;
using LearningWeb_Core.Generator;
using LearningWeb_Core.Senders;
using LearningWeb_Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace LearningSite.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IViewRenderService _viewRenderService;

        public AccountController(IUserServices userServices, IViewRenderService viewRenderService)
        {
            _userServices = userServices;
            _viewRenderService = viewRenderService;
        }

        #region Register

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel register)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(register);
            //}

            if (_userServices.IsEmailExist(FixingObject.FixingEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نمی باشد");
                return View(register);
            }

            if (_userServices.IsUserNameExist(register.userName))
            {
                ModelState.AddModelError("UserName", "نام کاربری معتبر نمی باشد");
                return View(register);
            }

            User user = new User()
            {
                UserName = register.userName,
                Email = FixingObject.FixingEmail(register.Email),
                ActivateCode = UniqCode.GenerateUniqCode(),
                IsActive = false,
                Password = register.Password,
                RegisterDate = DateTime.Now,
                UserAvatar = "Defualt.png",
            };

            _userServices.AddUser(user);

            #region Send Activation Email

            string Body = _viewRenderService.RenderToStringAsync("_ActivationEmail", user);
            SendEmail.Send(user.Email, "ایمیل فعال سازی", Body);

            #endregion

            return View("SuccesRegister", user);
        }

        #endregion

        #region Login

        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.ErrorMessage = null;
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(LoginViewModel NewUser)
        {
            if (!ModelState.IsValid)
            {
                return View(NewUser);
            }

            var User = _userServices.loginUser(NewUser);

            ViewBag.IsSuccess = "False";
            if (User != null)
            {
                if (User.Password.Equals(NewUser.Password))
                {
                    if (User.IsActive)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()),
                            new Claim(ClaimTypes.Name, User.UserName),
                            //new Claim("IsAdmin", User..ToString()),
                            // new Claim("CodeMeli", user.Email),

                        };
                        
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        
                        var principal = new ClaimsPrincipal(identity);

                        var properties = new AuthenticationProperties
                        {
                            IsPersistent = NewUser.RemmeberMe
                        };

                        HttpContext.SignInAsync(principal, properties);

                        ViewBag.IsSuccess = "True";
                        return View();
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "کاربر فعال نمی باشد";
                        return View();
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "نام کاربری با رمز عبور تطابق ندارد";
                    return View();
                }

            }

            ViewBag.ErrorMessage = "کاربر یافت نشد";
            return View();
        }

        #endregion

        #region ActiveAccount

        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userServices.ActiveUser(id);
            return View("ActivateAccount");
        }

        #endregion

        #region Logout

        [Route("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/home/index");
        }

        #endregion

        #region ForgotPassword

        [Route("/ForgotPassword")]
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [Route("/ForgotPassword")]
        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgotPassword)
        {
            if (!ModelState.IsValid)
                return View(forgotPassword);

            string fixedEmail = FixingObject.FixingEmail(forgotPassword.Email);
            var User = _userServices.GetUserByEmail(fixedEmail);
            if (User == null)
            {
                ModelState.AddModelError("Email", "کاربری با ایمیل وارد شده یافت نشد!");
                return View();
            }

            string Email_body = _viewRenderService.RenderToStringAsync("_ForgotPasswordEmail", User);
            SendEmail.Send(User.Email, "تغییر رمز عبور", Email_body);

            ViewBag.IsSuccess = true;
            return View();
        }

        #endregion

        #region ResetPassword

        //[Route("/ResetPassword/{activecode}")]
        //[HttpGet]
        public IActionResult ResetPassword(string activecode)
        {
            return View(new ResetPasswordViewModel
            {
                ActiveCode = activecode
            });
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            string ActiveCode = resetPasswordViewModel.ActiveCode;
            string NewPassword = resetPasswordViewModel.Password;

            if (!ModelState.IsValid)
                return View();

            var User = _userServices.getUserByActiveCode(ActiveCode);
            if (User == null)
                return NotFound();

            _userServices.ResetPassword(ActiveCode, NewPassword);

            return View();
        }

        #endregion



    }
}

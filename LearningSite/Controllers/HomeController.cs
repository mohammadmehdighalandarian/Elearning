using LearningSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LearningWeb_Core.Services;
using Microsoft.AspNetCore.Authorization;

namespace LearningSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly ICourseService _courseService;

        public HomeController(IUserServices userServices, ICourseService courseService)
        {
            _userServices = userServices;
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            return View(_courseService.GetAllCourseForIndex());
        }

        
        public IActionResult ContactUs()
        {
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("/OnlinePayment/{id}")]
        public IActionResult OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];

                var wallet = _userServices.GetWalletByWalletId(id);

                var payment = new ZarinpalSandbox.Payment(wallet.Amount);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    ViewBag.code = res.RefId;
                    ViewBag.IsSuccess = true;
                    wallet.IsPay = true;
                    _userServices.UpdateWallet(wallet);
                }

            }

            return View();
        }


    }
}
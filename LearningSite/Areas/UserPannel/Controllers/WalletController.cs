using DataLayer.Entities.Wallet;
using LearningWeb_Core.DTOs.UserPanel.Wallet;
using LearningWeb_Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningSite.Areas.UserPannel.Controllers
{
    [Area("UserPannel")]
    [Authorize]
    public class WalletController : Controller
    {
        private readonly IUserServices _userServices;

        public WalletController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [Route("/UserPannel/Wallet")]
        public IActionResult Wallet()
        {
            ViewBag.ListOfTransactions = _userServices.GetAllTransactions(User.Identity.Name);
            return View();
        }

        [Route("/UserPannel/Wallet")]
        [HttpPost]
        public IActionResult Wallet(ChargeWalletViewModel charge)
        {
            string UserName = User.Identity.Name;

            if (!ModelState.IsValid)
            {
                return View(charge);
            }
                
            
           long walletId= _userServices.ChargeWallet(UserName,charge.Amount,"واریز");

            #region Online Payment

            var payment = new ZarinpalSandbox.Payment(charge.Amount);

            var Response = payment.PaymentRequest("شارژ کیف پول", "https://localhost:7089/OnlinePayment/" + walletId, "mohammadmehdighn@gmail.com", "09217586752");

            if (Response.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + Response.Result.Authority);
            }

            #endregion

            ViewBag.IsSuccess = "true";
            ViewBag.ListOfTransactions = _userServices.GetAllTransactions(UserName);

            return View();
        }

    }
}

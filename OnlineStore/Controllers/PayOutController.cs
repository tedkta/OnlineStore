using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data.Models.PayOut;
using OnlineStore.Services.PayOut;
using OnlineStore.Services.Users;

namespace OnlineStore.Controllers;

public class PayOutController : Controller
{

        private readonly IPayOutService payOutService;
        private readonly LoggedUserService loggedUserService;

        public PayOutController(IPayOutService payOutService, LoggedUserService loggedUserService)
        {
            this.payOutService = payOutService;
            this.loggedUserService = loggedUserService;
        }

 
        public ActionResult CheckOut()
        {
            int userId = loggedUserService.User.Id;
            PayOutViewModel model = payOutService.BuildPayoutModel(userId);
            return View(model);
        }


        [HttpPost]
        public ActionResult CheckOut(PayOutViewModel model)
        {
            int userId = loggedUserService.User.Id;

            

            payOutService.PlacePayout(userId, model);
            return RedirectToAction("Confirmation");
        }

        public ActionResult Confirmation()
        {
            return View();
        }
}
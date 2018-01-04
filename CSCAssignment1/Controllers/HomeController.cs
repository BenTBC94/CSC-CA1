using CSCAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Stripe;

namespace CSCAssignment1.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    ViewBag.Title = "Home Page";

        //    return View();
        //}

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Charge()
        {
            ViewBag.Message = "Learn how to process payments with Stripe";

            return View(new StripeChargeModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Charge(StripeChargeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var chargeId = await ProcessPayment(model);
            return View("Index");
        }

        private async Task<string> ProcessPayment(StripeChargeModel model)
        {
            return await Task.Run(() =>
            {
                var myCharge = new StripeChargeCreateOptions
                {
                    // convert the amount of £12.50 to pennies i.e. 1250
                    Amount = (int)(model.Amount * 100),
                    Currency = "gbp",
                    Description = "Description for test charge",
                    SourceCard = new StripeSourceOptions
                    {
                        TokenId = model.Token
                    }
                };

                var chargeService = new StripeChargeService("your private key here");
                var stripeCharge = chargeService.Create(myCharge);

                return stripeCharge.Id;
            });
        }
    }
}

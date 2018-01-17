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

        public ActionResult Payment()
        {
            return View();
        }

        public ActionResult Charge(string stripeEmail, string stripeToken)
        {
            var customerService = new StripeCustomerService();
            var chargeService = new StripeChargeService();
            var customer = customerService.Create(new StripeCustomerCreateOptions
            {
                Email = stripeEmail,
                SourceToken = stripeToken
            });
            var charge = chargeService.Create(new StripeChargeCreateOptions
            {
                Amount = 500,
                Description = "ASP.NET Stripe Tutorial",
                Currency = "usd",
                CustomerId = customer.Id
            });
            return View();
        }

    }
}

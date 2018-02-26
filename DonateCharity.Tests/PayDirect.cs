using DonateCharity.Controllers;
using DonateCharity.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace DonateCharity.Tests.Controllers
{

    [TestClass]
    public class PayDirect
    {
       DirectPayController controller;
       string APIkey = "F9802CL6tBYFeInWkTdR/ZTnii9NgHEz7fZNVCrWf0FSTnfXlMBxdM0lgJD5QSYSoOqUlI";
        string Password = "Abcd1234";
        string EndPoint = "https://api.sandbo.ewaypayments.com";
        DirectPay modelSuccess = new DirectPay()
            {
               Email="rek@yf.com",
               CardName="Testing",
               CardNumber="4444333322221111",
               CardExpiryMonth="02",
               CardExpiryYear ="2019",
               CVN="100",
               TotalAmount =100
            };
        DirectPay modelFailure = new DirectPay()
        {
            Email = "rek@yf.com",
            CardName = "Testing",
            CardNumber = "4444333322221",
            CardExpiryMonth = "02",
            CardExpiryYear = "2019",
            CVN = "10",
            TotalAmount = 100
        };

        [TestMethod]
        public void PaymentSuccessful()
        {
            controller = new DirectPayController();
            var res = (RedirectToRouteResult)controller.DonatingDetails(modelSuccess);
            Assert.AreEqual("Payment Success!!!", res.RouteValues["Status"]);
        }
        [TestMethod]
        public void PaymentFailure()
        {
            controller = new DirectPayController();
            var res = (RedirectToRouteResult)controller.DonatingDetails(modelFailure);
            Assert.AreEqual("Payment Failure!!!", res.RouteValues["Status"]);
        }
        //[TestMethod]
        //public void IsEndPointValid()
        //{
        //    ewayClient = RapidClientFactory.NewRapidClient(APIKEY, PASSWORD, ENDPOINT);
        //    controller = new DirectPayController();
        //    var res = (RedirectToRouteResult)controller.DonatingDetails(modelDP);
        //    Assert.AreEqual("S9991", res.RouteValues["Errors"]);
        //}
        
    }
}

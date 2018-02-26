using DonateCharity.Models;
using eWAY.Rapid;
using eWAY.Rapid.Enums;
using eWAY.Rapid.Models;
using System;
using System.Web.Mvc;

namespace DonateCharity.Controllers
{
    public class DirectPayController : Controller
    {
        private string APIKEY = "F9802CL6tBYFeInWkTdR/ZTnii9NgHEz7fZNVCrWf0FSTnfXlMBxdM0lgJD5QSYSoOqUlI";
        private string PASSWORD = "Abcd1234";
        private string ENDPOINT = "https://api.sandbox.ewaypayments.com";
        private string Message;
        private Result resultModel;
 
        // GET: DirectPay
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DonatingDetails(DirectPay directPayModel)
        {
           IRapidClient ewayClient = RapidClientFactory.NewRapidClient(APIKEY, PASSWORD, ENDPOINT);
            Transaction transaction = new Transaction()
            {
                Customer = new Customer()
                {
                    Title = directPayModel.Title,
                    FirstName = directPayModel.FirstName,
                    LastName = directPayModel.LastName,
                    Email = directPayModel.Email,
                    CardDetails = new CardDetails()
                    {
                        Name = directPayModel.CardName,
                        Number = directPayModel.CardNumber,

                        ExpiryMonth = directPayModel.CardExpiryMonth,

                        ExpiryYear = directPayModel.CardExpiryYear,
                        CVN = directPayModel.CVN,
                    }
                },
                PaymentDetails = new PaymentDetails()
                {
                    TotalAmount = directPayModel.TotalAmount
                },
                TransactionType = TransactionTypes.Purchase,
            };
            CreateTransactionResponse response = ewayClient.Create(PaymentMethod.Direct, transaction);
            if (response.Errors != null)
            {
                foreach (string errorCode in response.Errors)
                {
                    if (errorCode == "V6100")
                    {
                        Message = "Error message: Invalid Credit card name entered, please check the name and try again!!!.";
                    }
                    else if (errorCode == "V6101" || errorCode == "V6102")
                    {
                        Message = "Error message: Invalid Expiryd Date entered,please check the date and try again!!!.";
                    }
                    else if (errorCode == "V6106")
                    {
                        Message = "Error message: Invalid CVN entered,please check the CVN and try again!!!.";
                    }
                    else if (errorCode == "V6110")
                    {
                        Message = "Error message: Invalid card number entered,please check the cardnumber and try again!!!.";
                    }
                    resultModel= new Result { Status = "Payment Failure!!!", Description = Message };
                }
            }
            else
            {
                if ((bool)response.TransactionStatus.Status)
                {
                    Message = ("Thanks for donating to Charity. Please note your transaction ID: " + response.TransactionStatus.TransactionID);
                    resultModel = new Result { Status = "Payment Success!!!",Description = Message };
                }
            }
            return RedirectToAction("Result", "Results",resultModel);
        }
        [HttpPost]
        public ActionResult PaymentDetails(DirectPay model, IRapidClient eway)
        {
            DirectPay paymodel = new DirectPay
            {
                //customer details
                Title = Request["txt-title"].ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                //card details
                CardName = model.CardName,
                CardNumber = model.CardNumber,
                CardExpiryMonth = Request["txt-cardmonth"].ToString(),
                CardExpiryYear = Request["txt-cardyear"].ToString(),
                CVN = model.CVN,
                //donation amount
                TotalAmount = Convert.ToInt16(model.TotalAmount)
            };
            return RedirectToAction("DonatingDetails", "DirectPay", paymodel);
        }
    }     
}
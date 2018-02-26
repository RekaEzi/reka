using System.Web.Mvc;
using DonateCharity.Models;

namespace DonateCharity.Controllers
{
    public class ResultsController : Controller
    {
        // GET: Results
        public ActionResult Result(Result resultmodel)
        {
            return View(resultmodel);
        }
    }
}
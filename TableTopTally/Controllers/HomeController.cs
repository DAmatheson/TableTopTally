/* HomeController.cs
 * Purpose: Main controller for the angular app. Everything for Angular to handle gets sent to Index()
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.25: Created
*/ 

using System.Web.Mvc;

namespace TableTopTally.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
using System.Web.Mvc;

namespace WebApiSample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return RedirectToRoute("HelpPage_Default");
        }
    }
}

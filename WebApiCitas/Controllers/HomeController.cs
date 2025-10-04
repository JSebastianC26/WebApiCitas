using System.Web.Mvc;

namespace WebApiCitas.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home";

            return View();
        }
    }
}

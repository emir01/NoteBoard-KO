using System.Web.Mvc;

namespace NK.Web.Controllers
{
    /// <summary>
    /// The basic Home Controller returning the basic home view
    /// </summary>
    public class HomeController : Controller
    {
        //
        // Get /Home/Index
        public ActionResult Index()
        {
            // The basic home view with the app description and login/register buttons.
            return View();
        }
    }
}

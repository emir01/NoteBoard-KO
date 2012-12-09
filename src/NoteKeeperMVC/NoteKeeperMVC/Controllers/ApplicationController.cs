using System.Web.Mvc;

namespace NK.Web.Controllers
{
    /// <summary>
    /// The controller that returns the basic main view containing the user boards.
    /// </summary>
    [Authorize]
    public class ApplicationController : Controller
    {
        #region Actions
        
        //
        // GET: /Application/
        public ActionResult Index()
        {
            // Just return the index view for the application controller.
            // The view renders the basic layout elements and uses knockout for basic client side logic.
            return View();
        }

        #endregion
    }
}

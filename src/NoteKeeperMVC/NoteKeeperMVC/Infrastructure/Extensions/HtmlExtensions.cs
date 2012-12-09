using System.Web;
using System.Web.Mvc;

namespace NK.Web.Infrastructure.Extensions
{
    public static class HtmlExtensions
    {
        /// <summary>
        /// Html helper that returns a boolean value indicating if there is a logged in user.
        /// </summary>
        public static bool UserLoggedIn(this HtmlHelper helper)
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }
    }
}
using System.Web.Mvc;

namespace NK.Web
{
    /// <summary>
    /// Global MVC Filter configuration.
    /// </summary>
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
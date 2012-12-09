using System.Web.Optimization;

namespace NK.Web
{
    /// <summary>
    ///  The bundle configurator, that configures script and style bundles.
    /// </summary>
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundle/css").Include(
                "~/Content/Bootstrap/bootstrap.less",
                "~/Content/styling/less/site.less",
                "~/Content/loadmask/jquery.loadmask.css",
                "~/Content/themes/ui-lightness/*.css"
            ));

            var scriptBundle = new ScriptBundle("~/bundle/js").Include(
                "~/Scripts/*.js",
                "~/Scripts/komvvm/customBindings/*.js",
                "~/Scripts/komvvm/repos/*.js",
                "~/Scripts/komvvm/viewModels/*.js",
                "~/Scripts/src/Modules/*.js",
                "~/Scripts/src/Utilities/*.js"
                );

            bundles.Add(scriptBundle);
            
            bundles.Add(new ScriptBundle("~/bundle/bootstrapjs").Include(
                "~/Scripts/bootstrap/bootstrap-alert.js",        
                "~/Scripts/bootstrap/bootstrap-carousel.js",        
                "~/Scripts/bootstrap/bootstrap-collapse.js",        
                "~/Scripts/bootstrap/bootstrap-dropdown.js",        
                "~/Scripts/bootstrap/bootstrap-modal.js",        
                "~/Scripts/bootstrap/bootstrap-scrollspy.js",        
                "~/Scripts/bootstrap/bootstrap-tab.js",        
                "~/Scripts/bootstrap/bootstrap-tooltip.js",        
                "~/Scripts/bootstrap/bootstrap-transition.js",        
                "~/Scripts/bootstrap/bootstrap-typehead.js"
            ));
        }
    }
}
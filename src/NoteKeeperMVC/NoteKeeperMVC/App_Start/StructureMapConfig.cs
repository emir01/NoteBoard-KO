using System.Web.Http;
using System.Web.Mvc;
using NK.Web.DependencyResolution;
using StructureMap;

namespace NK.Web
{
    /// <summary>
    /// The Structure Map configuration bundled in this Config class.
    /// </summary>
    public static class StructureMapConfig
    {
        public static void Start(HttpConfiguration config, ControllerBuilder builder)
        {
            // innitialize the IoC using Structure Map.
            IoC.Initialize();

            var container = ObjectFactory.Container;
            
            // Standard settings for SM for MVC4.
            config.DependencyResolver = new SmDependencyResolver(container);

            // Used as a test to overide some issues with Injecting dependenices on Api controllers.
            // Though the Api controllers are not used in this version of the application
            builder.SetControllerFactory(new StructureMapControllerFactory());
        }
    }
}
using NK.Model.Repositories.Interface;
using NK.Services.Interface;
using NK.Web.Infrastructure.Authentication;
using StructureMap;

namespace NK.Web
{
    /// <summary>
    /// The main IoC class controlling Structure Map functionality for the entire Web Application
    /// </summary>
    public static class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.Assembly(typeof(INoteService).Assembly); // Scan the Service Project assembly for extra usage specifications
                                        scan.Assembly(typeof(INoteRepository).Assembly); // Scan the Model Project assembly for extra usage specifications
                                        scan.WithDefaultConventions();
                                    });
                            // Specification of concrete implementation usage
                            x.For<IAuthenticationWrapper>().Use<CookieAuthentication>();
                        });
            return ObjectFactory.Container;
        }
    }
}
using NK.Services.Interface;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace NK.Services.Infrastructure
{
    /// <summary>
    /// The IoC registy for Structure Map used to define the SM usagees for the Services Project.
    /// This gets "scanned" by the main web project IoC configuration.
    /// </summary>
    public class IoCInit:Registry
    {
        public IoCInit()
        {
            ObjectFactory.Configure(x =>
            {
                x.For<INoteService>().Use<NoteService>();
                x.For<IUserService>().Use<UserService>();
            });
        }        
    }
}

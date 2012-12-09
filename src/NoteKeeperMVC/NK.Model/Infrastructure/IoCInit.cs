using NK.Model.Repositories;
using NK.Model.Repositories.Interface;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace NK.Model.Infrastructure
{
    /// <summary>
    /// The IoC registy for Structure Map used to define the SM usagees for the Model Project.
    /// This gets "scanned" by the main web project IoC configuration.
    /// </summary>
    public class IoCInit:Registry
    {
        public IoCInit()
        {
            ObjectFactory.Configure(x =>
            {
                x.For<INoteRepository>().Use<NoteRepository>();
                x.For<IBoardRepository>().Use<BoardRepository>();
                x.For<IUserRepository>().Use<UserRepository>();
            });
        }        
    }
}

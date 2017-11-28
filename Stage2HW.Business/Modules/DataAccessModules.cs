using Ninject.Modules;
using Stage2HW.DataAccess.Repositories;
using Stage2HW.DataAccess.Repositories.Interfaces;

namespace Stage2HW.Business.Modules
{
    public class DataAccessModules : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserRepository>().To<UserRepository>();
        }
    }
}

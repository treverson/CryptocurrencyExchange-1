using Ninject.Modules;
using Stage2HW.Business.Services;
using Stage2HW.Business.Services.Interfaces;

namespace Stage2HW.Business.Modules
{
    public class BusinessModules : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserService>();
            Bind<ICurrencyGenerator>().To<CurrencyGenerator>().InSingletonScope();
        }
    }
}

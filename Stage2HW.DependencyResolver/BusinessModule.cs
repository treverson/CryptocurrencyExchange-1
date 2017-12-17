using Ninject.Modules;
using Stage2HW.Business.Services;
using Stage2HW.Business.Services.Interfaces;

namespace Stage2HW.DependencyResolver
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserService>();
            Bind<ICurrencyGenerator>().To<CurrencyGenerator>().InSingletonScope();
            Bind<IExchangeRatesProvider>().To<ExchangeRatesProvider>().InSingletonScope();
            Bind<ITransactionService>().To<TransactionService>();
        }
    }
}

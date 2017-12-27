using Ninject.Modules;
using Stage2HW.Business.Services;
using Stage2HW.Business.Services.Interfaces;

namespace Stage2HW.DependencyResolver
{
    public class BusinessModule : NinjectModule
    {
        private readonly ICurrencyExchangeConfig _currencyExchangeConfig;

        public BusinessModule(ICurrencyExchangeConfig currencyExchangeConfig)
        {
            _currencyExchangeConfig = currencyExchangeConfig;
        }

        public override void Load()
        {
            Bind<IUserService>().To<UserService>();

            if (_currencyExchangeConfig.ExchangeType == "BitBay")
            {
                Bind<IExchangeRatesProvider>().To<ExchangeRatesProvider>().InSingletonScope();
            }
            else
            {
                Bind<IExchangeRatesProvider>().To<CurrencyGenerator>().InSingletonScope();
            }

            Bind<ITransactionService>().To<TransactionService>();
        }
    }
}

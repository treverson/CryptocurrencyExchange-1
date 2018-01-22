using Ninject;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.DependencyResolver;
using Stage2HW.WebApi.Configuration;

namespace Stage2HW.WebApi.Bootstrap
{
    class NinjectBootstrap
    {
        public static readonly ICurrencyExchangeConfig CurrencyExchangeConfig = new AppConfig();

        public static IKernel GetKernel()
        {
            var kernel = new StandardKernel(new BusinessModule(CurrencyExchangeConfig), new DataAccessModule(CurrencyExchangeConfig));

            return kernel;
        }
    }
}

using Ninject.Modules;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.WebApi.Configuration;

namespace Stage2HW.WebApi.Modules
{
    public class WebApiModules : NinjectModule
    {
        private readonly ICurrencyExchangeConfig _currencyExchangeConfig;

        public WebApiModules(ICurrencyExchangeConfig currencyExchangeConfig)
        {
            _currencyExchangeConfig = currencyExchangeConfig;
        }
        public override void Load()
        {
            Bind<ICurrencyExchangeConfig>().To<AppConfig>();
;       }
    }
}

using Ninject.Modules;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.Cli.Configuration;
using Stage2HW.Cli.IoHelpers;
using Stage2HW.Cli.IoHelpers.Interfaces;
using Stage2HW.Cli.Menu;
using Stage2HW.Cli.Menu.Interfaces;
using Stage2HW.Cli.Menu.MenuOptions;
using Stage2HW.Cli.Services;
using Stage2HW.Cli.Services.Interfaces;

namespace Stage2HW.Cli.Modules
{
    public class CliModules : NinjectModule
    {
        private readonly ICurrencyExchangeConfig _currencyExchangeConfig;

        public CliModules(ICurrencyExchangeConfig currencyExchangeConfig)
        {
            _currencyExchangeConfig = currencyExchangeConfig;
        }
        public override void Load()
        {
            Bind<IConsoleWriter>().To<ConsoleWriter>();
            Bind<IInputReader>().To<InputReader>();
            Bind<IMenu>().To<StartUpMenu>();
            Bind<IMenu>().To<MainMenu>().WhenInjectedInto<LogInToExchange>();
            Bind<IRegisterToExchange>().To<RegisterToExchange>();
            Bind<IValidateInput>().To<ValidateInput>();

            if (_currencyExchangeConfig.ExchangeType == "BitBay")
            {
                Bind<ICryptocurrencyExchange>().To<CryptocurrencyExchange>();
            }
            else
            {
                Bind<ICryptocurrencyExchange>().To<DummyCryptocurrencyExchange>();
            }

            Bind<ILogInToExchange>().To<LogInToExchange>();
            Bind<ICurrencyExchangeConfig>().To<AppConfig>();
            Bind<IAccountOperations>().To<AccountOperations>();
            Bind<IShowUser>().To<ShowUser>().InSingletonScope();
;       }
    }
}

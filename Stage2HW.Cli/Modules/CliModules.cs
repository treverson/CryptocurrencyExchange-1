using Ninject.Modules;
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
    internal class CliModules : NinjectModule
    {
        public override void Load()
        {
            Bind<IConsoleWriter>().To<ConsoleWriter>();
            Bind<IInputReader>().To<InputReader>();
            Bind<IMenu>().To<StartUpMenu>();
            Bind<IMenu>().To<MainMenu>().WhenInjectedInto<LogInToExchange>();
            Bind<IRegisterToExchange>().To<RegisterToExchange>();
            Bind<IValidateInput>().To<ValidateInput>();
            Bind<ICryptocurrencyExchange>().To<CryptocurrencyExchange>();
            Bind<ILogInToExchange>().To<LogInToExchange>();

            //new ones
            Bind<IExchangeRatesDownloader>().To<ExchangeRatesDownloader>();
        }
    }
}

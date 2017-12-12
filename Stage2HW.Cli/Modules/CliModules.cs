using Ninject.Modules;
using Stage2HW.Business.Services;
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
        public override void Load()
        {
            Bind<IConsoleWriter>().To<ConsoleWriter>();
            Bind<IInputReader>().To<InputReader>();
            Bind<IMenu>().To<StartUpMenu>();
            Bind<IMenu>().To<MainMenu>().WhenInjectedInto<LogInToExchange>();
            Bind<IRegisterToExchange>().To<RegisterToExchange>();
            Bind<IValidateInput>().To<ValidateInput>();
            Bind<IDummyCryptocurrencyExchange>().To<DummyCryptocurrencyExchange>();
            Bind<ILogInToExchange>().To<LogInToExchange>();
            Bind<ICryptocurrencyExchange>().To<CryptocurrencyExchange>();

            //new ones
            Bind<ICurrencyExchangeConfig>().To<AppConfig>();
        }
    }
}

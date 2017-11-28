using Ninject.Modules;
using Stage2HW.Cli.IoHelpers;
using Stage2HW.Cli.IoHelpers.Interfaces;
using Stage2HW.Cli.Menu;
using Stage2HW.Cli.Menu.Interfaces;
using Stage2HW.Cli.Menu.MenuOptions;

namespace Stage2HW.Cli.Modules
{
    class CliModules : NinjectModule
    {
        public override void Load()
        {
            Bind<IConsoleWriter>().To<ConsoleWriter>();
            Bind<IInputReader>().To<InputReader>();
            Bind<IMenu>().To<MainMenu>();
            Bind<IRegisterToExchange>().To<RegisterToExchange>();
            Bind<IValidateInput>().To<ValidateInput>();
        }
    }
}

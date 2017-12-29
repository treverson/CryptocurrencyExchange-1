using Ninject;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.Cli.Configuration;
using Stage2HW.Cli.Modules;
using Stage2HW.DependencyResolver;

namespace Stage2HW.Cli
{
    internal class Program
    {
        public static readonly ICurrencyExchangeConfig CurrencyExchangeConfig = new AppConfig();

        private static void Main()
        {
            IKernel kernel = new StandardKernel(new BusinessModule(CurrencyExchangeConfig), new DataAccessModule(CurrencyExchangeConfig), new CliModules(CurrencyExchangeConfig));
            
            var programLoop = kernel.Get<ProgramLoop>();

            programLoop.Run();
        }
    }
}

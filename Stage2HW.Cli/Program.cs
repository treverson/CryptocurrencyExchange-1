using Ninject;
using Stage2HW.Cli.Modules;
using Stage2HW.DependencyResolver;

namespace Stage2HW.Cli
{
    internal class Program
    {
        static void Main()
        {
            IKernel kernel = new StandardKernel(new BusinessModule(), new DataAccessModule(), new CliModules());
            
            var programLoop = kernel.Get<ProgramLoop>();

            programLoop.Run();
        }
    }
}

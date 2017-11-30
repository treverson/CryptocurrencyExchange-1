using Ninject;
using Stage2HW.Business.Modules;
using Stage2HW.Cli.Modules;

namespace Stage2HW.Cli
{
    internal class Program
    {
        static void Main()
        {
            IKernel kernel = new StandardKernel(new CliModules(), new BusinessModules(), new DataAccessModules());

            var programLoop = kernel.Get<ProgramLoop>();

            programLoop.Run();
        }
    }
}

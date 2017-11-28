using Ninject;
using Stage2HW.Business.Modules;
using Stage2HW.Cli.Modules;

namespace Stage2HW.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new CliModules(), new BusinessModules(), new DataAccessModules());

            var programLoop = kernel.Get<ProgramLoop>();

            programLoop.Run();
        }
    }
}

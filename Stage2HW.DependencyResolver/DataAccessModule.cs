using Ninject.Modules;
using Stage2HW.DataAccess.Repositories;
using Stage2HW.DataAccess.Repositories.Interfaces;

namespace Stage2HW.DependencyResolver
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserRepository>().To<UserRepository>();
            Bind<ITransactionRepository>().To<CloudTransactionRepository>();
            // Bind<ITransactionRepository>().To<TransactionRepository>();
        }
    }
}

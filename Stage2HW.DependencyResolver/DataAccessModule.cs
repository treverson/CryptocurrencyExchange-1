using Ninject.Modules;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.DataAccess.Repositories;
using Stage2HW.DataAccess.Repositories.Interfaces;

namespace Stage2HW.DependencyResolver
{
    public class DataAccessModule : NinjectModule
    {
        private readonly ICurrencyExchangeConfig _currencyExchangeConfig;

        public DataAccessModule(ICurrencyExchangeConfig currencyExchangeConfig)
        {
            _currencyExchangeConfig = currencyExchangeConfig;
        }

        public override void Load()
        {
            Bind<IUserRepository>().To<UserRepository>();
            if (_currencyExchangeConfig.DataBaseType == "Azure")
            {
                Bind<ITransactionRepository>().To<CloudTransactionRepository>();
            }
            if(_currencyExchangeConfig.DataBaseType == "Sql")
            {
                Bind<ITransactionRepository>().To<TransactionRepository>();
            }
        }
    }
}

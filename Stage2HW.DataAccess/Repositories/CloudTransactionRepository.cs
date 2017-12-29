using AutoMapper;
using Microsoft.WindowsAzure.Storage.Table;
using Stage2HW.DataAccess.Models;
using Stage2HW.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stage2HW.DataAccess.Repositories
{
    public class CloudTransactionRepository : BaseAzureTableStorageRepository, ITransactionRepository
    {
        private IMapper _mapper;
       
        public CloudTransactionRepository() : base("UserTransactions")
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Transaction, CloudTransaction>();
                cfg.CreateMap<CloudTransaction, Transaction>();
            });
            _mapper = config.CreateMapper();
        }

        public IEnumerable<Transaction> GetTransactionsHistory(int activeUserId)
        {
            List<Transaction> temp = new List<Transaction>();

            var cloudTransactions =  TableReference.CreateQuery<CloudTransaction>().Where(t => t.UserId == activeUserId).ToList();
            foreach (var transaction in cloudTransactions)
            {
                temp.Add(_mapper.Map<CloudTransaction, Transaction>(transaction));
            }

            return temp;
        }

        public void RegisterTransaction(Transaction transaction)
        {
            var cloudTransaction = _mapper.Map<Transaction, CloudTransaction>(transaction);
            cloudTransaction.TransactionGuid = Guid.NewGuid();
            var operation = TableOperation.Insert(cloudTransaction);
            TableReference.Execute(operation);
        }

        public double GetUserCryptocurrencyBalance(string currencyName, int userId)
        {
            var foundCurrencies =  TableReference.CreateQuery<CloudTransaction>().Where(u=>u.UserId == userId).Where(c => c.CurrencyName == currencyName).ToList();
            return Math.Round(foundCurrencies.Sum(x => x.Amount), 7);
        }
    }
}

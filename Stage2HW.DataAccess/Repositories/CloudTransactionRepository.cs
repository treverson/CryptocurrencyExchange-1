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
        public CloudTransactionRepository() : base("usertransactions")
        {
        }

        public IEnumerable<Transaction> GetTransactionsHistory(int activeUserId)
        {
            return TableReference.CreateQuery<Transaction>().Where(t => t.UserId == activeUserId);
        }

        public void RegisterTransaction(Transaction transaction)
        {
            var operation = TableOperation.Insert(transaction);
            TableReference.Execute(operation);
        }

        public double GetUserCryptocurrencyBalance(string currencyName, int userId)
        {
            var foundCurrencies =  TableReference.CreateQuery<Transaction>().Where(u=>u.UserId == userId).Where(c => c.CurrencyName == currencyName).ToList();
            return Math.Round(foundCurrencies.Sum(x => x.Amount), 7);
        }
    }
}

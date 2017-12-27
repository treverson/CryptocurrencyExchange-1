using Stage2HW.DataAccess.Data;
using Stage2HW.DataAccess.Models;
using Stage2HW.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Stage2HW.DataAccess.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        public IEnumerable<Transaction> GetTransactionsHistory(int activeUserId)
        {
            using (var dbContext = new CryptocurrencyExchangeDbContext())
            {
                return dbContext.TransactionsDbSet.Where(t => t.UserId == activeUserId).OrderBy(d => d.TransactionDate).ToList();
            }
        }

        public void RegisterTransaction(Transaction transaction)
        {
            using (var dbContext = new CryptocurrencyExchangeDbContext())
            {
                dbContext.TransactionsDbSet.Add(transaction);
                dbContext.SaveChanges();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using Stage2HW.DataAccess.Models;
using Stage2HW.DataAccess.Repositories.Interfaces;

namespace Stage2HW.DataAccess.Repositories
{
    public class CloudTransactionRepository : BaseAzureTableStorageRepository, ITransactionRepository
    {
        public CloudTransactionRepository() : base("UserTransactions")
        { }

        public IEnumerable<Transaction> GetTransactionsHistory(int activeUserId)
        {
            return TableReference.CreateQuery<Transaction>().Where(t => t.UserId == activeUserId);
        }

        public void RegisterTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}

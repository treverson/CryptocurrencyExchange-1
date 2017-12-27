using System.Collections.Generic;
using Stage2HW.DataAccess.Models;

namespace Stage2HW.DataAccess.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        List<Transaction> GetTransactionsHistory(int activeUserId);
        void RegisterTransaction(Transaction transaction);
    }
}
using System.Collections.Generic;
using Stage2HW.DataAccess.Models;

namespace Stage2HW.DataAccess.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetTransactionsHistory(int activeUserId);
        void RegisterTransaction(Transaction transaction);
        double GetUserCryptocurrencyBalance(string currencyName, int userId);
        void DownloadHistory(string filePath, int activeUserId);
    }
}
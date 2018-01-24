using Stage2HW.Business.Dtos;
using System.Collections.Generic;

namespace Stage2HW.Business.Services.Interfaces
{
    public interface ITransactionService
    {
        List<TransactionDto> GetTransactionHistory(int activeUserId);
        void RegisterTransaction(TransactionDto transaction);
        double GetUserCryptocurrencyBalance(string currencyName, int userId);
        void DownloadHistory(string filePath, int activeUserId);
        UserRequest GetCryptocurrenciesBalance(int id);
        ExchangeRates GetExchangeRates();
    }
}
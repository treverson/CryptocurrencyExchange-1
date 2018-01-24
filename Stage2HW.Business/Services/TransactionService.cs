using AutoMapper;
using Stage2HW.Business.Dtos;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.DataAccess.Models;
using Stage2HW.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Stage2HW.Business.Services.Enums;

namespace Stage2HW.Business.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _iMapper;
        private readonly IExchangeRatesProvider _exchangeRatesProvider;

        public TransactionService(ITransactionRepository transactionRepository, IExchangeRatesProvider exchangeRatesProvider)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Transaction, TransactionDto>();
                cfg.CreateMap<TransactionDto, Transaction>();
            });
            _iMapper = config.CreateMapper();

            _transactionRepository = transactionRepository;
            _exchangeRatesProvider = exchangeRatesProvider;
            _exchangeRatesProvider.Run();
        }

        public void RegisterTransaction(TransactionDto transaction)
        {
            var transactionEntity = _iMapper.Map<TransactionDto, Transaction>(transaction);

            _transactionRepository.RegisterTransaction(transactionEntity);
        }

        public List<TransactionDto> GetTransactionHistory(int activeUserId)
        {
            var transactionsHistoryEntity = _transactionRepository.GetTransactionsHistory(activeUserId);

            var transactionsHistory = new List<TransactionDto>();

            foreach (var transaction in transactionsHistoryEntity)
            {
                var temp = _iMapper.Map<Transaction, TransactionDto>(transaction);

                transactionsHistory.Add(temp);
            }

            return transactionsHistory;
        }

        public double GetUserCryptocurrencyBalance(string currencyName, int userId)
        {
            return _transactionRepository.GetUserCryptocurrencyBalance(currencyName, userId);
        }

        public void DownloadHistory(string filePath, int activeUserId)
        {
            var transactions = GetTransactionHistory(activeUserId);

            File.WriteAllText(filePath, JsonConvert.SerializeObject(transactions, Formatting.Indented));
        }

        public UserRequest GetCryptocurrenciesBalance(int id)
        {
            UserRequest userRequest = new UserRequest();
            userRequest.OwnedCurrencies = new List<OwnedCurrency>();
            userRequest.User = new UserDto()
            {
                Id = id,
            };


            foreach (var currency in _exchangeRatesProvider.Currencies)
            {
                var userOwnedCurrency = new OwnedCurrency()
                {
                    Name = currency.CurrencyName.ToString(),
                    AvailableAmount = GetUserCryptocurrencyBalance(currency.CurrencyName.ToString(), id),
                };

                userRequest.OwnedCurrencies.Add(userOwnedCurrency);
            }

            return userRequest;
        }

        public ExchangeRates GetExchangeRates()
        {
            var newRates = new ExchangeRates()
            {
                BtcPrice = _exchangeRatesProvider.Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.Btc)
                    .LastPrice,
                BccPrice = _exchangeRatesProvider.Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.Bcc)
                    .LastPrice,
                EthPrice = _exchangeRatesProvider.Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.Eth)
                    .LastPrice,
                LtcPrice = _exchangeRatesProvider.Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.Ltc)
                    .LastPrice,
            };
            return newRates;
        }
    }
}

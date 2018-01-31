using System;
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
        private readonly IMapper _mapper;
        private readonly IExchangeRatesProvider _exchangeRatesProvider;

        public TransactionService(ITransactionRepository transactionRepository, IExchangeRatesProvider exchangeRatesProvider)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Transaction, TransactionDto>();
                cfg.CreateMap<TransactionDto, Transaction>();
            });
            _mapper = config.CreateMapper();

            _transactionRepository = transactionRepository;
            _exchangeRatesProvider = exchangeRatesProvider;
            _exchangeRatesProvider.Run();
        }

        public void RegisterTransaction(TransactionDto transaction)
        {
            var transactionEntity = _mapper.Map<TransactionDto, Transaction>(transaction);

            _transactionRepository.RegisterTransaction(transactionEntity);
        }

        public List<TransactionDto> GetTransactionHistory(int activeUserId)
        {
            var transactionsHistoryEntity = _transactionRepository.GetTransactionsHistory(activeUserId);

            var transactionsHistory = new List<TransactionDto>();

            foreach (var transaction in transactionsHistoryEntity)
            {
                var temp = _mapper.Map<Transaction, TransactionDto>(transaction);

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

            userRequest.OwnedCurrencies.Add(new OwnedCurrency()
            {
                Name = CurrencyNameEnum.Pln.ToString(),
               AvailableAmount = GetUserFiatBalance(id)
            });

            foreach (var currency in _exchangeRatesProvider.Currencies)
            {
                var userOwnedCurrency = new OwnedCurrency()
                {
                    Name = currency.CurrencyName.ToString(),
                    AvailableAmount = _transactionRepository.GetUserCryptocurrencyBalance(currency.CurrencyName.ToString(), id),
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

        public void RegisterFiatTransaction(TransactionDto transaction)
        {
            if (transaction.Amount < 0 && GetUserFiatBalance(transaction.UserId) < Math.Abs(transaction.Amount))
            {
                throw new Exception("Insufficient funds");
            }

            transaction.TransactionDate = DateTime.Now;
            transaction.Fiat = transaction.Amount;

            RegisterTransaction(transaction);
        }


        public void RegisterBuyTransaction(TransactionDto transaction)
        {
            if (transaction.Amount * transaction.ExchangeRate > GetUserFiatBalance(transaction.UserId))
            {
                throw new Exception("Insufficient funds");
            }

            transaction.TransactionDate = DateTime.Now;
            transaction.Fiat = -transaction.Amount * transaction.ExchangeRate;

            RegisterTransaction(transaction);
        }

        public void RegisterSellTransaction(TransactionDto transaction)
        {
            var availableCurrencies = GetCryptocurrenciesBalance(transaction.UserId).OwnedCurrencies;
            var chosenCurrencyAvailableAmount = availableCurrencies.SingleOrDefault(c => c.Name == transaction.CurrencyName.ToString()).AvailableAmount;

            if (chosenCurrencyAvailableAmount < Math.Abs(transaction.Amount))
            {
                throw new Exception("Insufficient funds");
            }
             
            transaction.Fiat = transaction.Amount * transaction.ExchangeRate;
            transaction.Amount = -transaction.Amount;
            transaction.TransactionDate = DateTime.Now;

            RegisterTransaction(transaction);
        }

        private double GetUserFiatBalance(int userId)
        {
            var fiat = _transactionRepository.GetUserFiatBalance(userId);
            return fiat;
        }
    }
}

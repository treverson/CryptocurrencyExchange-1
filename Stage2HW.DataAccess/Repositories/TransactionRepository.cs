using AutoMapper;
using Stage2HW.DataAccess.Data;
using Stage2HW.DataAccess.Models;
using Stage2HW.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stage2HW.DataAccess.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IMapper _mapper;

        public TransactionRepository()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Transaction, SqlTransaction>();
                cfg.CreateMap<SqlTransaction, Transaction>();
            });
            _mapper = config.CreateMapper();
        }

        public IEnumerable<Transaction> GetTransactionsHistory(int activeUserId)
        {
            List<Transaction> mappedTransactions = new List<Transaction>();

            using (var dbContext = new CryptocurrencyExchangeDbContext())
            {
                var transactionsFromSql = dbContext.TransactionsDbSet.Where(t => t.UserId == activeUserId).OrderBy(d => d.TransactionDate).ToList();
                foreach (var transaction in transactionsFromSql)
                {
                    mappedTransactions.Add(_mapper.Map<SqlTransaction, Transaction>(transaction));
                }
                return mappedTransactions; 
            }
        }

        public void RegisterTransaction(Transaction transaction)
        {
            using (var dbContext = new CryptocurrencyExchangeDbContext())
            {
                var sqlTransaction = _mapper.Map<Transaction, SqlTransaction>(transaction);
                dbContext.TransactionsDbSet.Add(sqlTransaction);
                dbContext.SaveChanges();
            }
        }

        public double GetUserCryptocurrencyBalance(string currencyName, int userId)
        {
            using (var dbContext = new CryptocurrencyExchangeDbContext())
            {
                var choosenCurrencyTransactionList =
                    dbContext.TransactionsDbSet.Where(c => c.CurrencyName == currencyName && c.UserId == userId).ToList();

                if (choosenCurrencyTransactionList.Count > 0)
                {
                    return Math.Round(choosenCurrencyTransactionList.Sum(x => x.Amount), 7);
                }
                else
                {
                    return 0;
                }
            }
        }

        public void RegisterWebTransaction(Transaction transactionEntity)
        {
            using (var dbContext = new CryptocurrencyExchangeDbContext())
            {
                
            }
        }

        public double GetUserFiatBalance(int id)
        {
            using (var dbContext = new CryptocurrencyExchangeDbContext())
            {
                return dbContext.TransactionsDbSet.Where(u => u.UserId == id).ToList().Sum(x=>x.Fiat);
            }
        }
    }
}

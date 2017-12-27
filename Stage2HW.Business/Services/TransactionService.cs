using AutoMapper;
using Stage2HW.Business.Dtos;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.DataAccess.Models;
using Stage2HW.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;

namespace Stage2HW.Business.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _iMapper;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Transaction, TransactionDto>();
                cfg.CreateMap<TransactionDto, Transaction>();
            });
            _iMapper = config.CreateMapper();

            _transactionRepository = transactionRepository;
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
    }
}

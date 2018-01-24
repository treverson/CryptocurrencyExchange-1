using Stage2HW.Business.Dtos;
using Stage2HW.Business.Services.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace Stage2HW.WebApi.Controllers
{
    public class UserTransactionsController : ApiController
    {
        private readonly ITransactionService _transactionService;

        public UserTransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        [ActionName("transactions")]
        public IEnumerable<TransactionDto> GetUserTransactions(int id)
        {
            return _transactionService.GetTransactionHistory(id);
        }

        [HttpGet]
        [ActionName("balance")]
        public UserRequest GetUserCryptocurrencies(int id)
        {
            return _transactionService.GetCryptocurrenciesBalance(id);
        }

        //[HttpGet]
        //[ActionName("balance")]
        //public UserRequest GetUserCryptocurrencies(int id)
        //{
        //    return _transactionService.GetCryptocurrenciesBalance(id);
        //}

        [HttpPost]
        public void RegisterTransaction(TransactionDto transaction)
        {
            _transactionService.RegisterTransaction(transaction);
        }
    }
}

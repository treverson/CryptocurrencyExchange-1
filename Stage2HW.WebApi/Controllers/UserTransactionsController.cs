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
        public IEnumerable<TransactionDto> GetUserTransactions(int id)
        {
            return _transactionService.GetTransactionHistory(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Stage2HW.Business.Dtos;
using Stage2HW.Business.Services.Interfaces;

namespace Stage2HW.WebApi.Controllers
{
    public class AccountController : ApiController
    {
        private readonly ITransactionService _transactionService;

        public AccountController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public UserRequest GetUserCryptocurrencies(int id)
        {
            return _transactionService.GetCryptocurrenciesBalance(id);
        }
    }
}

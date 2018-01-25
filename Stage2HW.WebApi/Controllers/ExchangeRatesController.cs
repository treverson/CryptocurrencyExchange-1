using System.Web.Http;
using Stage2HW.Business.Dtos;
using Stage2HW.Business.Services.Interfaces;

namespace Stage2HW.WebApi.Controllers
{
    public class ExchangeRatesController : ApiController
    {
        private readonly ITransactionService _transactionService;

        public ExchangeRatesController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public ExchangeRates GetExchangeRates()
        {
            var dupa = _transactionService.GetExchangeRates();
            return dupa;
        }
    }
}

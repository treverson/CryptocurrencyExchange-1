using Stage2HW.Business.Services.Interfaces;
using System.Web.Http;

namespace Stage2HW.WebApi.Controllers
{
    public class StatusController : ApiController
    {
        private readonly IWebApiCryptocurrencyExchange _webApiCryptocurrencyExchange;

        public StatusController(IWebApiCryptocurrencyExchange webApiCryptocurrencyExchange)
        {
            _webApiCryptocurrencyExchange = webApiCryptocurrencyExchange;
        }

        public string Get()
        {
            _webApiCryptocurrencyExchange.RunExchange();
            return "Currency exchange WebApi running";
        }
    }
}

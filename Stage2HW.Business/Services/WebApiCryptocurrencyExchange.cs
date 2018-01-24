using Stage2HW.Business.Services.Enums;
using Stage2HW.Business.Services.Interfaces;
using System.Linq;

namespace Stage2HW.Business.Services
{
    public class WebApiCryptocurrencyExchange : IWebApiCryptocurrencyExchange
    {
        private readonly IExchangeRatesProvider _exchangeRatesProvider;

        public WebApiCryptocurrencyExchange(IExchangeRatesProvider exchangeRatesProvider)
        {
            _exchangeRatesProvider = exchangeRatesProvider;

            _exchangeRatesProvider.Run();
        }

        public void RunExchange()
        {
            _exchangeRatesProvider.NewExchangeRatesEvent += UpdateValues;

            //_exchangeRatesProvider.NewExchangeRatesEvent -= UpdateValues;
        }
        
        public void UpdateValues(NewExchangeRatesEventArgs e)
        {
            _exchangeRatesProvider.Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.Btc).LastPrice =
                e.CurrenciesList.Single(c => c.CurrencyName == CurrencyNameEnum.Btc).LastPrice;
           

            _exchangeRatesProvider.Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.Bcc).LastPrice =
                e.CurrenciesList.Single(c => c.CurrencyName == CurrencyNameEnum.Bcc).LastPrice;
           

            _exchangeRatesProvider.Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.Eth).LastPrice =
                e.CurrenciesList.Single(c => c.CurrencyName == CurrencyNameEnum.Eth).LastPrice;

            _exchangeRatesProvider.Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.Ltc).LastPrice =
                e.CurrenciesList.Single(c => c.CurrencyName == CurrencyNameEnum.Ltc).LastPrice;

        }

    }
}

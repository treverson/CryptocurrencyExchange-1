using Newtonsoft.Json;
using Stage2HW.Business.Dtos;
using Stage2HW.Business.Services.Enums;
using Stage2HW.Business.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Timers;

namespace Stage2HW.Business.Services
{
    public class ExchangeRatesProvider : IExchangeRatesProvider
    {
        private readonly ICurrencyExchangeConfig _currencyExchangeConfig;

        public event NewExchangeRatesHandler NewExchangeRatesEvent;
     
        public List<Currency> Currencies { get; set; }

        public ExchangeRatesProvider(ICurrencyExchangeConfig currencyExchangeConfig)
        {
            _currencyExchangeConfig = currencyExchangeConfig;
            InitializeCryptocurrencies();
            GetRates();
        }

        public void Run()
        {
            Timer t = new Timer
            {
                Interval = _currencyExchangeConfig.RefreshTime
            };
            t.Start();
            t.Elapsed += StartDownload;
        }

        private void StartDownload(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            GetRates();
        }

        private void GetRates()
        {
            var httpClient = new HttpClient();

            var btcResponse = httpClient.GetAsync(_currencyExchangeConfig.BitBayBtcPlnAddress).Result.Content.ReadAsStringAsync().Result;
            var bccResponse = httpClient.GetAsync(_currencyExchangeConfig.BitBayBccPlnAddress).Result.Content.ReadAsStringAsync().Result;
            var ethResponse = httpClient.GetAsync(_currencyExchangeConfig.BitBayEthPlnAddress).Result.Content.ReadAsStringAsync().Result;
            var ltcResponse = httpClient.GetAsync(_currencyExchangeConfig.BitBayLtcPlnAddress).Result.Content.ReadAsStringAsync().Result;

            Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.Btc).LastPrice = JsonConvert.DeserializeObject<DownloadedData>(btcResponse).Last;
            Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.Bcc).LastPrice = JsonConvert.DeserializeObject<DownloadedData>(bccResponse).Last;
            Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.Eth).LastPrice = JsonConvert.DeserializeObject<DownloadedData>(ethResponse).Last;
            Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.Ltc).LastPrice = JsonConvert.DeserializeObject<DownloadedData>(ltcResponse).Last;

            var ratesDownloaded = new NewExchangeRatesEventArgs
            {
                CurrenciesList = Currencies
            };
            if(NewExchangeRatesEvent != null) NewExchangeRatesEvent.Invoke(ratesDownloaded);
        }

        private void InitializeCryptocurrencies()
        {
            Currency bitCoin = new Currency
            {
                CurrencyName = CurrencyNameEnum.Btc,
            };

            Currency bitCoinCash = new Currency
            {
                CurrencyName = CurrencyNameEnum.Bcc,
            };

            Currency ethereum = new Currency
            {
                CurrencyName = CurrencyNameEnum.Eth,
            };

            Currency liteCoin = new Currency
            {
                CurrencyName = CurrencyNameEnum.Ltc,
            };

            Currencies = new List<Currency>
            {
                bitCoin,
                bitCoinCash,
                ethereum,
                liteCoin
            };
        }
    }
}
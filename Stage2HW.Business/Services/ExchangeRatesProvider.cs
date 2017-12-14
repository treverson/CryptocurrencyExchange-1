using Newtonsoft.Json;
using Stage2HW.Business.Dtos;
using Stage2HW.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Timers;
using Stage2HW.Business.Services.Enums;

namespace Stage2HW.Business.Services
{
    public delegate void RatesDownloadedHandler(RatesDownloadedEventArgs e);

    public class ExchangeRatesProvider : IExchangeRatesProvider
    {
        private readonly ICurrencyExchangeConfig _currencyExchangeConfig;
        
       // private DownloadedData _btcData;
       // private DownloadedData _bccData;
       // private DownloadedData _ethData;
       // private DownloadedData _ltcData;

        //private List<Currency> _currencies = new List<Currency>();
       // public List<DownloadedData> _downloadedData;

        public event RatesDownloadedHandler NewRatesDownloadedEvent;
        public List<Currency> Currencies { get; set; }

        public ExchangeRatesProvider(ICurrencyExchangeConfig currencyExchangeConfig)
        {
            _currencyExchangeConfig = currencyExchangeConfig;
            InitializeCryptocurrencies();
            DownloadRates();
        }

        public void RunProvider()
        {
            Timer t = new Timer
            {
                Interval = _currencyExchangeConfig.RefreshTime
            };
            t.Start();
            t.Elapsed += StartDownload;
        }

        public void StartDownload(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            DownloadRates();
        }

        public void DownloadRates() //rename to GetRates
        {
            var httpClient = new HttpClient();

            var btcResponse = httpClient.GetAsync(_currencyExchangeConfig.BitBayBtcPlnAddress).Result.Content.ReadAsStringAsync().Result;
            var bccResponse = httpClient.GetAsync(_currencyExchangeConfig.BitBayBccPlnAddress).Result.Content.ReadAsStringAsync().Result;
            var ethResponse = httpClient.GetAsync(_currencyExchangeConfig.BitBayEthPlnAddress).Result.Content.ReadAsStringAsync().Result;
            var ltcResponse = httpClient.GetAsync(_currencyExchangeConfig.BitBayLtcPlnAddress).Result.Content.ReadAsStringAsync().Result;

            Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.BTC).Last = JsonConvert.DeserializeObject<DownloadedData>(btcResponse).Last;
            Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.BCC).Last = JsonConvert.DeserializeObject<DownloadedData>(bccResponse).Last;
            Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.ETH).Last = JsonConvert.DeserializeObject<DownloadedData>(ethResponse).Last;
            Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.LTC).Last = JsonConvert.DeserializeObject<DownloadedData>(ltcResponse).Last;

            var ratesDownloaded = new RatesDownloadedEventArgs
            {
                CurrenciesList = Currencies
            };

            if (NewRatesDownloadedEvent != null) NewRatesDownloadedEvent.Invoke(ratesDownloaded);

            //var responseList = new List<string>
            //{
            //    btcResponse,
            //    bccResponse,
            //    ethResponse,
            //    ltcResponse
            //};

            // SerializeData(responseList);
        }

        //public void SerializeData(List<string> responsesList)
        //{

        //    _btcData = JsonConvert.DeserializeObject<DownloadedData>(responsesList[0]);
        //    _bccData = JsonConvert.DeserializeObject<DownloadedData>(responsesList[1]);
        //    _ethData = JsonConvert.DeserializeObject<DownloadedData>(responsesList[2]);
        //    _ltcData = JsonConvert.DeserializeObject<DownloadedData>(responsesList[3]);

        //    //var downloadedData = new List<DownloadedData>
        //    //{
        //    //    _btcData,
        //    //    _bccData,
        //    //    _ethData,
        //    //    _ltcData
        //    //};
            
        // //   UpdateExchangeRates();
        //}
        
        /*********metoda do przemyslenia***********/
        //public void SerializeData(string response)
        //{
        //    var btcData = JsonConvert.DeserializeObject<CryptoCurrencyDownloadedData>(response);

        //}
        /*----------------------------------------*/

        //public void UpdateExchangeRates(/*DownloadedData btcData, DownloadedData bccData, DownloadedData ethData, DownloadedData ltcData*/)
       // {
            //Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.BTC).Ask = _btcData.Ask;
            //Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.BTC).Bid = _btcData.Bid;

            //Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.BCC).Ask = _bccData.Ask;
            //Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.BCC).Bid = _bccData.Bid;

            //Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.ETH).Ask = _ethData.Ask;
            //Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.ETH).Bid = _ethData.Bid;

            //Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.LTC).Ask = _ltcData.Ask;
            //Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.LTC).Bid = _ltcData.Bid;

            //var ratesDownloaded = new RatesDownloadedEventArgs
            //{
            //    CurrenciesList = Currencies
            //};

            //if(NewRatesDownloadedEvent != null) NewRatesDownloadedEvent.Invoke(ratesDownloaded);
      //  }

        ///<summary>
        ///PLN is a base currency
        ///that's why bid & ask are set to 1.
        ///</summary>
        private void InitializeCryptocurrencies()
        {
            Currency pln = new Currency
            {
                CurrencyName = CurrencyNameEnum.PLN,
                //Bid = 1,
                //Ask = 1
            };

            Currency bitCoin = new Currency
            {
                CurrencyName = CurrencyNameEnum.BTC,
                //Bid = _btcData.Bid,
                //Ask = _btcData.Ask
            };

            Currency bitCoinCash = new Currency
            {
                CurrencyName = CurrencyNameEnum.BCC,
                //Bid = _bccData.Bid,
                //Ask = _bccData.Ask
            };

            Currency ethereum = new Currency
            {
                CurrencyName = CurrencyNameEnum.ETH,
                //Bid = _ethData.Bid,
                //Ask = _ethData.Ask
            };

            Currency liteCoin = new Currency
            {
                CurrencyName = CurrencyNameEnum.LTC,
                //Bid = _ltcData.Bid,
                //Ask = _ltcData.Ask
            };

            Currencies = new List<Currency>
            {
                bitCoin,
                bitCoinCash,
                ethereum,
                liteCoin
            };

            // _currencies.Add(pln);
        }
    }
}
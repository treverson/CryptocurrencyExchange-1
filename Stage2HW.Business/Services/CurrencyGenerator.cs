using Stage2HW.Business.Services.Enums;
using Stage2HW.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Timers;
using Stage2HW.Business.Dtos;

namespace Stage2HW.Business.Services
{
    public enum Percentage
    {
        TenPercentDepreciation = -10,
        TenPercentAppreciation = 11,
    }

    public delegate void RatesGeneratedHandler(NewExchangeRatesEventArgs e);

    public class CurrencyGenerator : IExchangeRatesProvider
    {
        private readonly Random _randomizer = new Random();

        public List<Currency> Currencies { get; set; }

        public event NewExchangeRatesHandler NewExchangeRatesEvent;
      
        public void Run()
        {
            InitializeCryptocurrencies();

            Timer t = new Timer
            {
                Interval = 1000
            };
            t.Start();
            t.Elapsed += GenerateExchangeBehaviour;
        }

        public void GenerateExchangeBehaviour(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            foreach (var cryptocurrency in Currencies)
            {
                cryptocurrency.LastPrice = GenerateRates(cryptocurrency.LastPrice, cryptocurrency.MinValue,
                    cryptocurrency.MaxValue);
            }

            var ratesGenerated = new NewExchangeRatesEventArgs
            {
                CurrenciesList = Currencies
            };
            if (NewExchangeRatesEvent != null) NewExchangeRatesEvent.Invoke(ratesGenerated);
        }

        public double GenerateRates(double initialValue, int minValue, int maxValue)
        {
            double variationPercent = _randomizer.Next((int)Percentage.TenPercentDepreciation, (int)Percentage.TenPercentAppreciation) / 100.00;
            var currentValue = initialValue + initialValue * variationPercent;

            if (currentValue > maxValue)
            {
                return maxValue;
            }
            if (currentValue < minValue)
            {
                return minValue;
            }
            return currentValue;
        }

        private void InitializeCryptocurrencies()
        {
            Currency bitCoin = new Currency
            {
                CurrencyName = CurrencyNameEnum.Btc,
                MinValue = (int)ExchangeMaxMinValuesEnum.BtcMinValue,
                MaxValue = (int)ExchangeMaxMinValuesEnum.BtcMaxValue,
            };

            Currency bitCoinCash = new Currency
            {
                CurrencyName = CurrencyNameEnum.Bcc,
                MinValue = (int)ExchangeMaxMinValuesEnum.BccMinValue,
                MaxValue = (int)ExchangeMaxMinValuesEnum.BccMaxValue,
            };

            Currency ethereum = new Currency
            {
                CurrencyName = CurrencyNameEnum.Eth,
                MinValue = (int)ExchangeMaxMinValuesEnum.EthMinValue,
                MaxValue = (int)ExchangeMaxMinValuesEnum.EthMaxValue,
            };

            Currency liteCoin = new Currency
            {
                CurrencyName = CurrencyNameEnum.Ltc,
                MinValue = (int)ExchangeMaxMinValuesEnum.LtcMinValue,
                MaxValue = (int)ExchangeMaxMinValuesEnum.LtcMaxValue,
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

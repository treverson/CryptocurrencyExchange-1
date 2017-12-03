using Stage2HW.Business.Services.Enums;
using Stage2HW.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Timers;

namespace Stage2HW.Business.Services
{
    public delegate void RatesGeneratedHandler(RatesGeneratedEventArgs e);

    public class CurrencyGenerator : ICurrencyGenerator
    {
        private readonly List<CryptoCurrency> _cryptocurrencies = new List<CryptoCurrency>();

        private readonly Random _randomizer = new Random();

        public event RatesGeneratedHandler NewRatesGeneratedEvent;

        public void RunGenerator()
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
            foreach (var cryptoCurrency in _cryptocurrencies)
            {
                cryptoCurrency.Value = GenerateRates(cryptoCurrency.Value, cryptoCurrency.MinValue,
                    cryptoCurrency.MaxValue);
            }
            
            var ratesGenerated = new RatesGeneratedEventArgs
            {
                CurrenciesList = _cryptocurrencies
            };
            
            if (NewRatesGeneratedEvent != null) NewRatesGeneratedEvent.Invoke(ratesGenerated);
        }

        public double GenerateRates(double initialValue, int minValue, int maxValue)
        {
            double variation = _randomizer.Next(-10, 11) / 100.00;
            var currentValue = initialValue + initialValue * variation;

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
            CryptoCurrency bitCoin = new CryptoCurrency
            {
                Name = "BitCoin",
                MinValue = (int) ExchangeMaxMinValuesEnum.BtcMinValue,
                MaxValue = (int) ExchangeMaxMinValuesEnum.BtcMaxValue,
                Value = _randomizer.Next((int) ExchangeMaxMinValuesEnum.BtcMinValue, (int)ExchangeMaxMinValuesEnum.BtcMaxValue + 1)
            };

            CryptoCurrency bitCoinCash = new CryptoCurrency
            {
                Name = "BitCoinCash",
                MinValue = (int)ExchangeMaxMinValuesEnum.BccMinValue,
                MaxValue = (int)ExchangeMaxMinValuesEnum.BccMaxValue,
                Value = _randomizer.Next((int)ExchangeMaxMinValuesEnum.BccMinValue, (int)ExchangeMaxMinValuesEnum.BccMaxValue + 1)
            };

            CryptoCurrency ethereum = new CryptoCurrency
            {
                Name = "Ethereum",
                MinValue = (int)ExchangeMaxMinValuesEnum.EthMinValue,
                MaxValue = (int)ExchangeMaxMinValuesEnum.EthMaxValue,
                Value = _randomizer.Next((int)ExchangeMaxMinValuesEnum.EthMinValue, (int)ExchangeMaxMinValuesEnum.EthMaxValue + 1)
            };

            CryptoCurrency liteCoin = new CryptoCurrency
            {
                Name = "LiteCoin",
                MinValue = (int)ExchangeMaxMinValuesEnum.LtcMinValue,
                MaxValue = (int)ExchangeMaxMinValuesEnum.LtcMaxValue,
                Value = _randomizer.Next((int)ExchangeMaxMinValuesEnum.LtcMinValue, (int)ExchangeMaxMinValuesEnum.LtcMaxValue + 1)
            };

            _cryptocurrencies.Add(bitCoin);
            _cryptocurrencies.Add(bitCoinCash);
            _cryptocurrencies.Add(ethereum);
            _cryptocurrencies.Add(liteCoin);
        }
    }
}

using Stage2HW.Business.Services.Enums;
using Stage2HW.Business.Services.Interfaces;
using System;
using System.Timers;

namespace Stage2HW.Business.Services
{
    public delegate void RatesGeneratedHandler(RatesGeneratedEventArgs e);

    public class CurrencyGenerator : ICurrencyGenerator
    {
        private double _initialBtcValue;
        private double _initialBccValue;
        private double _initialEthValue;
        private double _initialLtcValue;

        public event RatesGeneratedHandler NewRatesEvent;

        private readonly Random _randomizer = new Random();

        public void RunGenerator()
        {
            _initialBtcValue = _randomizer.Next((int)ExchangeMaxMinValuesEnum.BtcMinValue, (int)ExchangeMaxMinValuesEnum.BtcMaxValue + 1);
            _initialBccValue = _randomizer.Next((int)ExchangeMaxMinValuesEnum.BccMinValue, (int)ExchangeMaxMinValuesEnum.BccMaxValue + 1);
            _initialEthValue = _randomizer.Next((int)ExchangeMaxMinValuesEnum.EthMinValue, (int)ExchangeMaxMinValuesEnum.EthMaxValue + 1);
            _initialLtcValue = _randomizer.Next((int)ExchangeMaxMinValuesEnum.LtcMinValue, (int)ExchangeMaxMinValuesEnum.LtcMaxValue + 1);
            
            Timer t = new Timer
            {
                Interval = 1000
            };
            t.Start();
            t.Elapsed += GenerateRates;
        }

        public void GenerateRates(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            double bitCoinValue = GenerateCryptoCurrency(_initialBtcValue, (int)ExchangeMaxMinValuesEnum.BtcMinValue, (int)ExchangeMaxMinValuesEnum.BtcMaxValue);
            double bitCoinCashValue = GenerateCryptoCurrency(_initialBccValue, (int)ExchangeMaxMinValuesEnum.BccMinValue, (int)ExchangeMaxMinValuesEnum.BccMaxValue);
            double ethereumValue = GenerateCryptoCurrency(_initialEthValue, (int)ExchangeMaxMinValuesEnum.EthMinValue, (int)ExchangeMaxMinValuesEnum.EthMaxValue);
            double liteCoinValue = GenerateCryptoCurrency(_initialLtcValue, (int)ExchangeMaxMinValuesEnum.LtcMinValue, (int)ExchangeMaxMinValuesEnum.LtcMaxValue);

            var ratesGenerated = new RatesGeneratedEventArgs
            {
                BitCoinValue = bitCoinValue,
                BitCoinCashValue= bitCoinCashValue,
                EthereumValue = ethereumValue,
                LiteCoinValue = liteCoinValue
            };

            if (NewRatesEvent != null) NewRatesEvent.Invoke(ratesGenerated);
        }

        public double GenerateCryptoCurrency(double initialValue, int minValue, int maxValue)
        {
            double variation = _randomizer.Next(-10, 11) / 100.00;

            return initialValue + initialValue * variation;
        }
    }
}

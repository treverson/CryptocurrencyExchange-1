using Stage2HW.Business.Services.Enums;
using Stage2HW.Business.Services.Interfaces;
using System;
using System.Timers;

namespace Stage2HW.Business.Services
{
    public delegate void RatesGeneratedHandler(RatesGeneratedEventArgs e);

    public class CurrencyGenerator : ICurrencyGenerator
    {
        private double _currentBtcValue;
        private double _currentBccValue;
        private double _currentEthValue;
        private double _currentLtcValue;
        private readonly Random _randomizer = new Random();

        public event RatesGeneratedHandler NewRatesGeneratedEvent;

        public void RunGenerator()
        {
            _currentBtcValue = _randomizer.Next((int)ExchangeMaxMinValuesEnum.BtcMinValue, (int)ExchangeMaxMinValuesEnum.BtcMaxValue + 1);
            _currentBccValue = _randomizer.Next((int)ExchangeMaxMinValuesEnum.BccMinValue, (int)ExchangeMaxMinValuesEnum.BccMaxValue + 1);
            _currentEthValue = _randomizer.Next((int)ExchangeMaxMinValuesEnum.EthMinValue, (int)ExchangeMaxMinValuesEnum.EthMaxValue + 1);
            _currentLtcValue = _randomizer.Next((int)ExchangeMaxMinValuesEnum.LtcMinValue, (int)ExchangeMaxMinValuesEnum.LtcMaxValue + 1);

            Timer t = new Timer
            {
                Interval = 1000
            };
            t.Start();
            t.Elapsed += GenerateRates;
        }

        public void GenerateRates(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            _currentBtcValue = GenerateCryptoCurrency(_currentBtcValue, (int)ExchangeMaxMinValuesEnum.BtcMinValue, (int)ExchangeMaxMinValuesEnum.BtcMaxValue);
            _currentBccValue = GenerateCryptoCurrency(_currentBccValue, (int)ExchangeMaxMinValuesEnum.BccMinValue, (int)ExchangeMaxMinValuesEnum.BccMaxValue);
            _currentEthValue = GenerateCryptoCurrency(_currentEthValue, (int)ExchangeMaxMinValuesEnum.EthMinValue, (int)ExchangeMaxMinValuesEnum.EthMaxValue);
            _currentLtcValue = GenerateCryptoCurrency(_currentLtcValue, (int)ExchangeMaxMinValuesEnum.LtcMinValue, (int)ExchangeMaxMinValuesEnum.LtcMaxValue);
            
            var ratesGenerated = new RatesGeneratedEventArgs
            {
                BitCoinValue = _currentBtcValue,
                BitCoinCashValue = _currentBccValue,
                EthereumValue = _currentEthValue,
                LiteCoinValue = _currentLtcValue
            };
            
            if (NewRatesGeneratedEvent != null) NewRatesGeneratedEvent.Invoke(ratesGenerated);
        }

        public double GenerateCryptoCurrency(double initialValue, int minValue, int maxValue)
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
    }
}

using System;
using System.ComponentModel;
using System.Threading;
using System.Timers;
using Stage2HW.Cli.IoHelpers.Interfaces;
using Stage2HW.Cli.Services.Interfaces;
using Timer = System.Timers.Timer;

namespace Stage2HW.Cli.Services
{
    internal class CryptocurrencyExchangeGenerator : ICryptocurrencyExchangeGenerator
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly IInputReader _inputReader;

        private static readonly Random ValueGenerator = new Random();
        private readonly Random _randomizer = new Random();
        private readonly double _initialBitCoinValue = ValueGenerator.Next(10000, 40001);
        private readonly double _initialBitCoinCashValue = ValueGenerator.Next(2000, 10001);
        private readonly double _initialEthereumValue = ValueGenerator.Next(1800, 2401);
        private readonly double _initialLiteCoinValue = ValueGenerator.Next(100, 401);
        private double _currentBitCoinValue;
        private double _currentBitCoinCashValue;
        private double _currentEthereumValue;
        private double _currentLiteCoinValue;

        public CryptocurrencyExchangeGenerator(IConsoleWriter consoleWriter, IInputReader inputReader)
        {
            _consoleWriter = consoleWriter;
            _inputReader = inputReader;
        }

        Timer t = new Timer
        {
            Interval = 1000
        };

        internal BackgroundWorker BgWorker = new BackgroundWorker
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };

        public void GenerateExchangeRates()
        {
            BgWorker.DoWork += BgWorkerChangeRates;

            do
            { 
                BgWorker.RunWorkerAsync();
            }
            while (_inputReader.ReadKey().Key != ConsoleKey.Escape);
            
            //if (_inputReader.ReadKey().Key == ConsoleKey.Escape)
            //{
            BgWorker.CancelAsync();
            t.Stop();
            //}
        }

        private void BgWorkerChangeRates(object sender, DoWorkEventArgs e)
        {
            t.Elapsed += OnElapsed;
            t.Start();

            //var worker = sender as BackgroundWorker;

            //if (worker.CancellationPending)
            //{
            //    BgWorker.CancelAsync();
            //    t.Stop();
            //}

            RunExchange();
        }

        private void RunExchange()
        {
            _consoleWriter.ClearConsole();
            _consoleWriter.WriteMessage("##### CRYPTOCURRENCY EXCHANGE #####\n");
            _consoleWriter.WriteMessage($"BTC: \n");
            _consoleWriter.WriteMessage($"BCC: \n");
            _consoleWriter.WriteMessage($"ETH: \n");
            _consoleWriter.WriteMessage($"LTC: ");
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            GenerateBitCoin();
            GenerateBitCoinCash();
            GenerateEthereum();
            GenerateLiteCoin();
            WriteNewValues();
        }

        public void WriteNewValues()
        {
            _consoleWriter.SetCursorPosition(5, 1);
            _consoleWriter.WriteMessage($"{_currentBitCoinValue.ToString("C")}");
            _consoleWriter.SetCursorPosition(5, 2);
            _consoleWriter.WriteMessage($"{_currentBitCoinCashValue.ToString("C")}");
            _consoleWriter.SetCursorPosition(5, 3);
            _consoleWriter.WriteMessage($"{_currentEthereumValue.ToString("C")}");
            _consoleWriter.SetCursorPosition(5, 4);
            _consoleWriter.WriteMessage($"{_currentLiteCoinValue.ToString("C")}");
        }

        public double GenerateBitCoin()
        {
            double variation = ValueGenerator.Next(1, 11) / 100.00;

            if (_randomizer.Next(0, 10) >= 5)
            {
                _currentBitCoinValue = _initialBitCoinValue + _initialBitCoinValue * variation;

                if (_currentBitCoinValue > (double)ExchangeMaxMinValuesEnum.BtcMaxValue)
                {
                    return _currentBitCoinValue = (double)ExchangeMaxMinValuesEnum.BtcMaxValue;
                }
                return _currentBitCoinValue;
            }

            _currentBitCoinValue = _initialBitCoinValue - _initialBitCoinValue * variation;
            if (_currentBitCoinValue < (double)ExchangeMaxMinValuesEnum.BtcMinValue)
            {
                return _currentBitCoinValue = (double)ExchangeMaxMinValuesEnum.BtcMinValue;
            }
            return _currentBitCoinValue;
        }

        public double GenerateBitCoinCash()
        {
            double variation = ValueGenerator.Next(1, 11) / 100.00;

            if (_randomizer.Next(0, 10) <= 5)
            {
                _currentBitCoinCashValue = _initialBitCoinCashValue + _initialBitCoinCashValue * variation;

                if (_currentBitCoinCashValue > (double)ExchangeMaxMinValuesEnum.BccMaxValue)
                {
                    return _currentBitCoinCashValue = (double)ExchangeMaxMinValuesEnum.BccMaxValue;
                }
                return _currentBitCoinCashValue;
            }

            _currentBitCoinCashValue = _initialBitCoinCashValue - _initialBitCoinCashValue * variation;
            if (_currentBitCoinCashValue < (double)ExchangeMaxMinValuesEnum.BccMinValue)
            {
                return _currentBitCoinCashValue = (double)ExchangeMaxMinValuesEnum.BccMinValue;
            }
            return _currentBitCoinCashValue;
        }

        public double GenerateEthereum()
        {
            var variation = ValueGenerator.Next(1, 11) / 100.00;

            if (_randomizer.Next(0, 10) > 5)
            {
                _currentEthereumValue = _initialEthereumValue + _initialEthereumValue * variation;

                if (_currentEthereumValue > (double)ExchangeMaxMinValuesEnum.EthMaxValue)
                {
                    return _currentEthereumValue = (double)ExchangeMaxMinValuesEnum.EthMaxValue;
                }
                return _currentEthereumValue;
            }

            _currentEthereumValue = _initialEthereumValue - _initialEthereumValue * variation;
            if (_currentEthereumValue < (double)ExchangeMaxMinValuesEnum.EthMinValue)
            {
                return _currentEthereumValue = (double)ExchangeMaxMinValuesEnum.EthMinValue;
            }
            return _currentEthereumValue;
        }

        public double GenerateLiteCoin()
        {
            var variation = ValueGenerator.Next(1, 11) / 100.00;

            if (_randomizer.Next(0, 10) < 5)
            {
                _currentLiteCoinValue = _initialLiteCoinValue + _initialLiteCoinValue * variation;

                if (_currentLiteCoinValue > (double)ExchangeMaxMinValuesEnum.LtcMaxValue)
                {
                    return _currentLiteCoinValue = (double)ExchangeMaxMinValuesEnum.LtcMaxValue;
                }
                return _currentLiteCoinValue;
            }

            _currentLiteCoinValue = _initialLiteCoinValue - _initialLiteCoinValue * variation;
            if (_currentLiteCoinValue < (double)ExchangeMaxMinValuesEnum.LtcMinValue)
            {
                return _currentLiteCoinValue = (double)ExchangeMaxMinValuesEnum.LtcMinValue;
            }
            return _currentLiteCoinValue;
        }
    }
}

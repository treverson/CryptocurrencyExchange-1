using Stage2HW.Business.Services;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.Cli.IoHelpers.Interfaces;
using System;
using System.Linq;
using Stage2HW.Business.Services.Enums;
using Stage2HW.Cli.Services.Interfaces;

namespace Stage2HW.Cli.Services
{
    internal class CryptocurrencyExchange : ICryptocurrencyExchange
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly IInputReader _inputReader;
        private readonly IExchangeRatesProvider _exchangeRatesProvider;

        public CryptocurrencyExchange(IConsoleWriter consoleWriter, IInputReader inputReader, ICurrencyGenerator currencyGenerator, IExchangeRatesProvider exchangeRatesProvider)
        {
            _inputReader = inputReader;
            _consoleWriter = consoleWriter;
            _exchangeRatesProvider = exchangeRatesProvider;

            _exchangeRatesProvider.RunProvider();
        }

        public void RunExchange()
        {
            PrintHeader();
            WriteNewValues();

            _exchangeRatesProvider.NewRatesDownloadedEvent += UpdateValues;

            while (_inputReader.ReadKey().Key != ConsoleKey.Escape)
            {
            }

            _exchangeRatesProvider.NewRatesDownloadedEvent -= UpdateValues;
        }

        private void PrintHeader()
        {
            _consoleWriter.ClearConsole();
            _consoleWriter.WriteMessage("############# CRYPTOCURRENCY EXCHANGE #############\n");
            _consoleWriter.WriteMessage("|    Currency     |        LAST       |\n");
        }
        
        public void UpdateValues(RatesDownloadedEventArgs e)
        {
            _exchangeRatesProvider.Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.BTC).Last =
                e.CurrenciesList.Single(c => c.CurrencyName == CurrencyNameEnum.BTC).Last;
            //_exchangeRatesProvider.Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.BTC).Bid =
            //    e.CurrenciesList.Single(c => c.CurrencyName == CurrencyNameEnum.BTC).Bid;

            _exchangeRatesProvider.Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.BCC).Last =
                e.CurrenciesList.Single(c => c.CurrencyName == CurrencyNameEnum.BCC).Last;
            //_exchangeRatesProvider.Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.BCC).Bid =
            //    e.CurrenciesList.Single(c => c.CurrencyName == CurrencyNameEnum.BCC).Bid;

            _exchangeRatesProvider.Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.ETH).Last =
                e.CurrenciesList.Single(c => c.CurrencyName == CurrencyNameEnum.ETH).Last;
            //_exchangeRatesProvider.Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.ETH).Bid =
            //    e.CurrenciesList.Single(c => c.CurrencyName == CurrencyNameEnum.ETH).Bid;

            _exchangeRatesProvider.Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.LTC).Last =
                e.CurrenciesList.Single(c => c.CurrencyName == CurrencyNameEnum.LTC).Last;
            //_exchangeRatesProvider.Currencies.Single(c => c.CurrencyName == CurrencyNameEnum.LTC).Bid =
            //    e.CurrenciesList.Single(c => c.CurrencyName == CurrencyNameEnum.LTC).Bid;

            WriteNewValues();
        }

        private void WriteNewValues()
        {
            int i = 3;
            foreach (var currency in _exchangeRatesProvider.Currencies)
            {
                _consoleWriter.SetCursorPosition(7, i);
                _consoleWriter.WriteMessage($"{currency.CurrencyName}");
                _consoleWriter.SetCursorPosition(21, i);
                _consoleWriter.WriteMessage($"{currency.Last.ToString("C")}");
                
                //_consoleWriter.SetCursorPosition(36, i);
                //_consoleWriter.WriteMessage($"{currency.Bid.ToString("C")}");
                i++;
            }

            _consoleWriter.WriteMessage($"\n\nPress ESC to go back");
        }
    }
}

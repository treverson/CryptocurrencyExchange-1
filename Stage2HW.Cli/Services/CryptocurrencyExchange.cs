using Stage2HW.Business.Services;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.Cli.IoHelpers.Interfaces;
using System;
using System.Linq;
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
            _consoleWriter.WriteMessage("|   Currency   |       ASK       |       BID      |\n");
        }

        public void UpdateValues(RatesDownloadedEventArgs e)
        {
            _exchangeRatesProvider.Currencies.Single(c => c.Name == "BitCoin").Ask =
                e.CurrenciesList.Single(c => c.Name == "BitCoin").Ask;
            _exchangeRatesProvider.Currencies.Single(c => c.Name == "BitCoin").Bid =
                e.CurrenciesList.Single(c => c.Name == "BitCoin").Bid;

            _exchangeRatesProvider.Currencies.Single(c => c.Name == "BitCoinCash").Ask =
                e.CurrenciesList.Single(c => c.Name == "BitCoinCash").Ask;
            _exchangeRatesProvider.Currencies.Single(c => c.Name == "BitCoinCash").Bid =
                e.CurrenciesList.Single(c => c.Name == "BitCoinCash").Bid;

            _exchangeRatesProvider.Currencies.Single(c => c.Name == "Ethereum").Ask =
                e.CurrenciesList.Single(c => c.Name == "Ethereum").Ask;
            _exchangeRatesProvider.Currencies.Single(c => c.Name == "Ethereum").Bid =
                e.CurrenciesList.Single(c => c.Name == "Ethereum").Bid;

            _exchangeRatesProvider.Currencies.Single(c => c.Name == "LiteCoin").Ask =
                e.CurrenciesList.Single(c => c.Name == "LiteCoin").Ask;
            _exchangeRatesProvider.Currencies.Single(c => c.Name == "LiteCoin").Bid =
                e.CurrenciesList.Single(c => c.Name == "LiteCoin").Bid;

            WriteNewValues();
        }

        private void WriteNewValues()
        {
            int i = 3;
            foreach (var currency in _exchangeRatesProvider.Currencies)
            {
                _consoleWriter.SetCursorPosition(2, i);
                _consoleWriter.WriteMessage($"{currency.Name}");
                _consoleWriter.SetCursorPosition(18, i);
                _consoleWriter.WriteMessage($"{currency.Ask.ToString("C")}");
                _consoleWriter.SetCursorPosition(36, i);
                _consoleWriter.WriteMessage($"{currency.Bid.ToString("C")}");
                i++;
            }

            _consoleWriter.WriteMessage($"\n\nPress ESC to go back");
        }
    }
}

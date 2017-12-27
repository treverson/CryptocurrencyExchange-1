using Stage2HW.Business.Services;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.Cli.IoHelpers.Interfaces;
using Stage2HW.Cli.Services.Interfaces;
using System;

namespace Stage2HW.Cli.Services
{
    internal class DummyCryptocurrencyExchange : ICryptocurrencyExchange
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly IInputReader _inputReader;
        private readonly IExchangeRatesProvider _exchangeRatesProvider;

        public DummyCryptocurrencyExchange(IConsoleWriter consoleWriter, IInputReader inputReader, IExchangeRatesProvider exchangeRatesProvider)
        {
            _inputReader = inputReader;
            _exchangeRatesProvider = exchangeRatesProvider;
            _consoleWriter = consoleWriter;
            
            _exchangeRatesProvider.Run();
        }

        public void RunExchange()
        {
            _consoleWriter.ClearConsole();
            _consoleWriter.WriteMessage("##### DUMMY CRYPTOCURRENCY EXCHANGE #####\n");
            _consoleWriter.WriteMessage("|  Currency    |       Price      |\n");

            _exchangeRatesProvider.NewExchangeRatesEvent += WriteNewValues;
            while (_inputReader.ReadKey().Key != ConsoleKey.Escape)
            {
            }
            _exchangeRatesProvider.NewExchangeRatesEvent -= WriteNewValues;
        }

        private void WriteNewValues(NewExchangeRatesEventArgs e)
        {
            int i = 3;

            foreach (var currency in e.CurrenciesList)
            {
                _consoleWriter.SetCursorPosition(3, i);
                _consoleWriter.WriteMessage($"{currency.CurrencyName}");
                _consoleWriter.SetCursorPosition(20, i);
                _consoleWriter.WriteMessage($"{currency.LastPrice:C}");

                i++;
            }

            _consoleWriter.WriteMessage($"\n\nPress ESC to go back");
        }
    }
}

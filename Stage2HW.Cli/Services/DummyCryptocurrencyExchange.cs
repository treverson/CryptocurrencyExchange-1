using Stage2HW.Business.Services;
using Stage2HW.Cli.IoHelpers.Interfaces;
using Stage2HW.Cli.Services.Interfaces;
using System;
using Stage2HW.Business.Services.Interfaces;

namespace Stage2HW.Cli.Services
{
    internal class DummyCryptocurrencyExchange : IDummyCryptocurrencyExchange
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly IInputReader _inputReader;
        private readonly ICurrencyGenerator _currencyGenerator;
        
        public DummyCryptocurrencyExchange(IConsoleWriter consoleWriter, IInputReader inputReader, ICurrencyGenerator currencyGenerator)
        {
            _inputReader = inputReader;
            _currencyGenerator = currencyGenerator;
            _consoleWriter = consoleWriter;

            _currencyGenerator.RunGenerator();
        }

        public void RunExchange()
        {
            _consoleWriter.ClearConsole();
            _consoleWriter.WriteMessage("##### DUMMY CRYPTOCURRENCY EXCHANGE #####\n");
            _consoleWriter.WriteMessage("|  Currency    |       Price      |\n");
            _currencyGenerator.NewRatesGeneratedEvent += WriteNewValues;

            while (_inputReader.ReadKey().Key != ConsoleKey.Escape)
            {
            }

            _currencyGenerator.NewRatesGeneratedEvent -= WriteNewValues;
        }

        private void WriteNewValues(RatesGeneratedEventArgs e)
        {
            int i = 3;

            foreach (var currency in e.CurrenciesList)
            {
                _consoleWriter.SetCursorPosition(3, i);
                _consoleWriter.WriteMessage($"{currency.Name}");
                _consoleWriter.SetCursorPosition(20, i);
                _consoleWriter.WriteMessage($"{currency.Value.ToString("C")}");

                i++;
            }

            _consoleWriter.WriteMessage($"\n\nPress ESC to go back");
        }
    }
}

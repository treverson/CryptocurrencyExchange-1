using Stage2HW.Business.Services;
using Stage2HW.Cli.IoHelpers.Interfaces;
using Stage2HW.Cli.Services.Interfaces;
using System;
using Stage2HW.Business.Services.Interfaces;

namespace Stage2HW.Cli.Services
{
    internal class CryptocurrencyExchange : ICryptocurrencyExchange
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly IInputReader _inputReader;
        private readonly ICurrencyGenerator _currencyGenerator;

        public CryptocurrencyExchange(IConsoleWriter consoleWriter, IInputReader inputReader, ICurrencyGenerator currencyGenerator)
        {
            _inputReader = inputReader;
            _currencyGenerator = currencyGenerator;
            _consoleWriter = consoleWriter;

            _currencyGenerator.RunGenerator();
        }

        public void RunExchange()
        {
            _consoleWriter.ClearConsole();
            _consoleWriter.WriteMessage("##### CRYPTOCURRENCY EXCHANGE #####");
            
            _currencyGenerator.NewRatesGeneratedEvent += WriteNewValues;

            while (_inputReader.ReadKey().Key != ConsoleKey.Escape)
            {
            }

            _currencyGenerator.NewRatesGeneratedEvent -= WriteNewValues;
        }

        private void WriteNewValues(RatesGeneratedEventArgs e)
        {
            int i = 2;
            do
            {
                foreach (var currency in e.CurrenciesList)
                {
                    _consoleWriter.SetCursorPosition(0, i);
                    _consoleWriter.WriteMessage($"{currency.Name}: {currency.Value.ToString("C")}");
                    i++;
                }
            }
            while (i <= e.CurrenciesList.Count+1);
            _consoleWriter.WriteMessage($"\n\nPress ESC to go back");
        }
    }
}

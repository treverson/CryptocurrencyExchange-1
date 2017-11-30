using Stage2HW.Business.Services;
using Stage2HW.Cli.IoHelpers.Interfaces;
using Stage2HW.Cli.Services.Interfaces;
using System;

namespace Stage2HW.Cli.Services
{
    internal class CryptocurrencyExchange : ICryptocurrencyExchange
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly IInputReader _inputReader;

        private readonly CurrencyGenerator _generator = new CurrencyGenerator();

        public CryptocurrencyExchange(IConsoleWriter consoleWriter, IInputReader inputReader)
        {
            _inputReader = inputReader;
            _consoleWriter = consoleWriter;
            _generator.RunGenerator();
        }

        public void RunExchange()
        {
            _consoleWriter.ClearConsole();
            _consoleWriter.WriteMessage("##### CRYPTOCURRENCY EXCHANGE #####\n");

            _consoleWriter.WriteMessage($"BTC: \n");
            _consoleWriter.WriteMessage($"BCC: \n");
            _consoleWriter.WriteMessage($"ETH: \n");
            _consoleWriter.WriteMessage($"LTC: ");
            
             _generator.NewRatesEvent += WriteNewValues;

            while (_inputReader.ReadKey().Key != ConsoleKey.Escape)
            {
            }

            _generator.NewRatesEvent -= WriteNewValues;
        }

        private void WriteNewValues(RatesGeneratedEventArgs e)
        {
            _consoleWriter.SetCursorPosition(5, 1);
            _consoleWriter.WriteMessage($"{e.BitCoinValue.ToString("C")}");
            _consoleWriter.SetCursorPosition(5, 2);
            _consoleWriter.WriteMessage($"{e.BitCoinCashValue.ToString("C")}");
            _consoleWriter.SetCursorPosition(5, 3);
            _consoleWriter.WriteMessage($"{e.EthereumValue.ToString("C")}");
            _consoleWriter.SetCursorPosition(5, 4);
            _consoleWriter.WriteMessage($"{e.LiteCoinValue.ToString("C")}");
        }
    }
}

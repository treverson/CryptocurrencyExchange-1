using Stage2HW.Business.Dtos;
using Stage2HW.Business.Services.Enums;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.Cli.IoHelpers.Interfaces;
using Stage2HW.Cli.Menu.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
[assembly: InternalsVisibleTo("Stage2HW.Tests")]
namespace Stage2HW.Cli.Menu.MenuOptions
{
    internal class AccountOperations : IAccountOperations
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly IValidateInput _validateInput;
        private readonly IExchangeRatesProvider _exchangeRatesProvider;
        private readonly IShowUser _showUser;
        private readonly ITransactionService _transactionService;

        private readonly IInputReader _inputReader;

        public AccountOperations(IConsoleWriter consoleWriter, IInputReader inputReader, IValidateInput validateInput, IShowUser showUser, IExchangeRatesProvider exchangeRatesProvider, ITransactionService transactionService)
        {
            _consoleWriter = consoleWriter;
            _validateInput = validateInput;
            _showUser = showUser;
            _exchangeRatesProvider = exchangeRatesProvider;
            _transactionService = transactionService;
            _inputReader = inputReader;
        }

        public void DepositFunds()
        {
            DisplayHeader();

            _consoleWriter.WriteMessage("Enter deposit amount (PLN): ");
            var depositAmount = _validateInput.ValidateAmount();

            var deposit = new TransactionDto
            {
                Amount = depositAmount,
                CurrencyName = CurrencyNameEnum.Pln.ToString(),
                TransactionDate = DateTime.Now,
                UserId = _showUser.ActiveUser.Id,
                Fiat = depositAmount
            };

            _transactionService.RegisterTransaction(deposit);

            _consoleWriter.WriteMessage($"Deposit successfully registered!");
            _validateInput.PauseLoop();
        }

        public void WithdrawFunds()
        {
            DisplayHeader();
            var accountBalance = GetAccountBalance();

            _consoleWriter.WriteMessage($"Your account balance: {accountBalance:C}");

            _consoleWriter.WriteMessage("\nEnter withdrawal amount (PLN): ");
            var withdrawalAmount = _validateInput.ValidateAmount();

            while (withdrawalAmount > accountBalance)
            {
                _consoleWriter.WriteMessage("Not enough funds. Press ENTER to try again or press ESCAPE to go back.");

                var userPressed = _inputReader.ReadKey();

                if (userPressed.Key == ConsoleKey.Escape)
                    return;
                if (userPressed.Key == ConsoleKey.Enter)
                {
                    _consoleWriter.WriteMessage("\nEnter withdrawal amount (PLN): ");
                    withdrawalAmount = _validateInput.ValidateAmount();
                }
            }

            var withdrawal = new TransactionDto
            {
                Amount = -withdrawalAmount,
                CurrencyName = CurrencyNameEnum.Pln.ToString(),
                TransactionDate = DateTime.Now,
                UserId = _showUser.ActiveUser.Id,
                Fiat = -withdrawalAmount
            };
            _transactionService.RegisterTransaction(withdrawal);

            _consoleWriter.WriteMessage($"Withdrawal successfull!");
            _validateInput.PauseLoop();
        }

        internal double GetAccountBalance()
        {
            return GetHistory().Sum(a => a.Fiat);
        }

        public void ViewHistory()
        {
            DisplayHeader();

            var transactionHistory = GetHistory();
            _consoleWriter.WriteMessage($"\nFiat (PLN) balance: {transactionHistory.Sum(a => a.Fiat):C}\n");

            _consoleWriter.WriteMessage("Transactions history: \n");

            _consoleWriter.DisplayHistoryHeader();

            _consoleWriter.DisplayTransactionsHistory(transactionHistory);

            _consoleWriter.WriteMessage("\n\nPress ENTER to continue.");
            _inputReader.WaitForEnter();
        }

        internal List<TransactionDto> GetHistory()
        {
            return _transactionService.GetTransactionHistory(_showUser.ActiveUser.Id);
        }

        public void BuyCurrencies()
        {
            DisplayHeader();
            _consoleWriter.WriteMessage("# Buy cryptocurrencies\n");

            _consoleWriter.WriteMessage("Choose cryptocurrency to buy: \n");

            var i = 1;
            foreach (var currency in _exchangeRatesProvider.Currencies)
            {
                _consoleWriter.WriteMessage($"{i}. {currency.CurrencyName}, Last price: {currency.LastPrice:C}\n");
                i++;
            }

            var userChosenCurrency = GetUserCurrencyChoice();

            _consoleWriter.WriteMessage($"\nBuying {userChosenCurrency.CurrencyName}, Last price: {userChosenCurrency.LastPrice:C}\nEnter amount to buy: ");
            var buyAmount = _validateInput.ValidateAmount();

            var userFiatBalance = GetAccountBalance();

            while (buyAmount * userChosenCurrency.LastPrice > userFiatBalance)
            {
                _consoleWriter.WriteMessage("Not enough funds. Press ENTER to try again or press ESCAPE to go back.");

                var userPressed = _inputReader.ReadKey();

                if (userPressed.Key == ConsoleKey.Escape)
                {
                    return;
                }
                if (userPressed.Key == ConsoleKey.Enter)
                {
                    _consoleWriter.WriteMessage("\nEnter amount to buy: ");
                    buyAmount = _validateInput.ValidateAmount();
                }
            }

            TransactionDto boughtCurrency = new TransactionDto
            {
                Amount = buyAmount,
                CurrencyName = userChosenCurrency.CurrencyName.ToString(),
                TransactionDate = DateTime.Now,
                ExchangeRate = userChosenCurrency.LastPrice,
                UserId = _showUser.ActiveUser.Id,
                Fiat = -buyAmount * userChosenCurrency.LastPrice
            };

            _transactionService.RegisterTransaction(boughtCurrency);

            _consoleWriter.WriteMessage($"Bought {boughtCurrency.Amount} {boughtCurrency.CurrencyName} for {-boughtCurrency.Fiat:C}");
            _validateInput.PauseLoop();
        }

        public void SellCurrencies()
        {
            DisplayHeader();
            _consoleWriter.WriteMessage("# Sell cryptocurrencies\n");

            var userOwnedCurrencies = new List<Currency>();
            var temp = GetHistory();

            var i = 1;
            foreach (var currency in _exchangeRatesProvider.Currencies)
            {
                var amount = Math.Round(temp.Where(c => c.CurrencyName == currency.CurrencyName.ToString()).Sum(a => a.Amount), 7);

                if (amount > 0)
                {
                    _consoleWriter.WriteMessage($"{i}. {currency.CurrencyName}\n");
                    userOwnedCurrencies.Add(currency);
                    i++;
                }
            }

            if (userOwnedCurrencies.Count == 0)
            {
                _consoleWriter.WriteMessage("You don't have any currencies to sell\n");
                _validateInput.PauseLoop();
                return;
            }

            _consoleWriter.WriteMessage("Choose cryptocurrency to sell: \n");

            var userChosenCurrency = GetCurrencyFromUserTransactions(userOwnedCurrencies);
            var userChosenCurrencyBalance = _transactionService.GetUserCryptocurrencyBalance(userChosenCurrency.CurrencyName.ToString(), _showUser.ActiveUser.Id);

            _consoleWriter.WriteMessage($"\nSelling { userChosenCurrency.CurrencyName}, last price: {userChosenCurrency.LastPrice:C}\nCurrent balance: {userChosenCurrencyBalance}\nEnter amount to sell: ");
            var sellAmount = Math.Round(_validateInput.ValidateAmount(), 7);

            while (sellAmount > userChosenCurrencyBalance)
            {
                _consoleWriter.WriteMessage("\nNot enough coins. Press ENTER to try again or press ESCAPE to go back.");

                var userPressed = _inputReader.ReadKey();

                if (userPressed.Key == ConsoleKey.Escape)
                {
                    return;
                }
                if (userPressed.Key == ConsoleKey.Enter)
                {
                    _consoleWriter.WriteMessage("\nEnter amount to sell: ");
                    sellAmount = _validateInput.ValidateAmount();
                }
            }

            var soldCurrency = new TransactionDto
            {
                Amount = -sellAmount,
                CurrencyName = userChosenCurrency.CurrencyName.ToString(),
                TransactionDate = DateTime.Now,
                ExchangeRate = userChosenCurrency.LastPrice,
                UserId = _showUser.ActiveUser.Id,
                Fiat = sellAmount * userChosenCurrency.LastPrice
            };

            _transactionService.RegisterTransaction(soldCurrency);

            _consoleWriter.WriteMessage($"Sold {-soldCurrency.Amount} {soldCurrency.CurrencyName} for {soldCurrency.Fiat:C}");
            _validateInput.PauseLoop();
        }

        public void SaveHistory()
        {
            _consoleWriter.WriteMessage($"\nSpecify download path (e.g. C:\\Users\\{Environment.UserName}\\Desktop): ");

            var filePath = _inputReader.ReadInput();

            try
            {
                _transactionService.DownloadHistory(filePath + $"\\{_showUser.ActiveUser.Login} transactions history.json", _showUser.ActiveUser.Id);

                _consoleWriter.WriteMessage("\nDownload successful.");
                _validateInput.PauseLoop();
            }
            catch
            {
                _consoleWriter.WriteMessage($"Encountered an error with the download path.\nTry e.g. C:\\Users\\{Environment.UserName}\\Desktop");
                _validateInput.PauseLoop();
            }
        }

        internal Currency GetCurrencyFromUserTransactions(List<Currency> userCurrencies)
        {
            Currency currency = null;
            do
            {
                var userInput = _inputReader.ReadKey();
                int.TryParse(userInput.KeyChar.ToString(), out int choice);
                if (choice != 0 && choice <= userCurrencies.Count)
                {
                    currency = userCurrencies[choice - 1];
                }
                else
                {
                    _consoleWriter.WriteMessage("\nInvalid option, choose again.");
                }
            } while (currency == null);

            return currency;
        }

        internal Currency GetUserCurrencyChoice()
        {
            Currency chosenCurrency = null;

            do
            {
                var userInput = _inputReader.ReadKey();
                int.TryParse(userInput.KeyChar.ToString(), out int choice);
                if (choice != 0 && choice <= _exchangeRatesProvider.Currencies.Count)
                {
                    chosenCurrency = _exchangeRatesProvider.Currencies[choice - 1];
                }
                else
                {
                    _consoleWriter.WriteMessage("\nInvalid option, choose again.");
                }
            } while (chosenCurrency == null);

            return chosenCurrency;
        }

        internal void DisplayHeader()
        {
            _consoleWriter.ClearConsole();
            _consoleWriter.WriteMessage("############# CRYPTOCURRENCY EXCHANGE #############\n");
            _consoleWriter.WriteMessage($"# Logged in as: {_showUser.ActiveUser.Login}\n");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Stage2HW.Business.Dtos;
using Stage2HW.Business.Services.Enums;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.Cli.IoHelpers.Interfaces;
using Stage2HW.Cli.Menu.Interfaces;

namespace Stage2HW.Cli.Menu.MenuOptions
{
    internal class AccountOperations : IAccountOperations
    {
        private readonly IUserService _userService;
        private readonly IConsoleWriter _consoleWriter;
        private readonly IValidateInput _validateInput;
        private readonly IExchangeRatesProvider _exchangeRatesProvider;
        private readonly IShowUser _showUser;

        private readonly IInputReader _inputReader;

        public AccountOperations(IUserService userService, IConsoleWriter consoleWriter, IInputReader inputReader, IValidateInput validateInput, IShowUser showUser, IExchangeRatesProvider exchangeRatesProvider)
        {
            _userService = userService;
            _consoleWriter = consoleWriter;
            _validateInput = validateInput;
            _showUser = showUser;
            _exchangeRatesProvider = exchangeRatesProvider;
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

            _userService.RegisterTransaction(deposit);

            _consoleWriter.WriteMessage($"Deposit successfully registered!");
            _validateInput.PauseLoop();
        }

        public void WithdrawFunds()
        {
            DisplayHeader();
            var accountBalance = GetAccountBalance();

            _consoleWriter.WriteMessage($"Your account balance: {accountBalance.ToString("C")}");

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
            _userService.RegisterTransaction(withdrawal);

            _consoleWriter.WriteMessage($"\nWithdrawal successfull!");
            _validateInput.PauseLoop();
        }

        internal double GetAccountBalance()
        {
            return GetHistory().Sum(a => a.Fiat);
        }

        public void ViewHistory()
        {
            DisplayHeader();
            _consoleWriter.WriteMessage("Transactions history: \n");

            var transactionHistory = GetHistory();
            DisplayHistoryHeader();
            var i = 5;
            foreach (var transaction in transactionHistory)
            {
                if (transaction.CurrencyName == CurrencyNameEnum.Pln.ToString())
                {
                    _consoleWriter.SetCursorPosition(2, i);
                    _consoleWriter.WriteMessage($"{transaction.Id}");
                    _consoleWriter.SetCursorPosition(10, i);
                    _consoleWriter.WriteMessage($"{transaction.CurrencyName}");
                    _consoleWriter.SetCursorPosition(27, i);
                    _consoleWriter.WriteMessage($"n/a");
                    _consoleWriter.SetCursorPosition(45, i);
                    _consoleWriter.WriteMessage($"{transaction.Amount}");
                    _consoleWriter.SetCursorPosition(62, i);
                    _consoleWriter.WriteMessage($"{transaction.Fiat:C}");
                    _consoleWriter.SetCursorPosition(80, i);
                    _consoleWriter.WriteMessage($"{transaction.TransactionDate}");
                }
                else
                {
                    _consoleWriter.SetCursorPosition(2, i);
                    _consoleWriter.WriteMessageInColor($"{transaction.Id}");
                    _consoleWriter.SetCursorPosition(10, i);
                    _consoleWriter.WriteMessageInColor($"{transaction.CurrencyName}");
                    _consoleWriter.SetCursorPosition(24, i);
                    _consoleWriter.WriteMessageInColor($"{transaction.ExchangeRate:C}");
                    _consoleWriter.SetCursorPosition(45, i);
                    _consoleWriter.WriteMessageInColor($"{transaction.Amount}");
                    _consoleWriter.SetCursorPosition(62, i);
                    _consoleWriter.WriteMessageInColor($"{transaction.Fiat:C}");
                    _consoleWriter.SetCursorPosition(80, i);
                    _consoleWriter.WriteMessageInColor($"{transaction.TransactionDate}");
                }
                i++;
            }
            _consoleWriter.WriteMessage("\n\nPress ENTER to continue.");
            _inputReader.WaitForEnter();
        }

        internal List<TransactionDto> GetHistory()
        {
            return _userService.GetTransactionHistory(_showUser.ActiveUser.Id);
        }

        internal void DisplayHistoryHeader()
        {
            _consoleWriter.SetCursorPosition(1, 3);
            _consoleWriter.WriteMessage("Id |");
            _consoleWriter.SetCursorPosition(5, 3);
            _consoleWriter.WriteMessage("Currency Name |");
            _consoleWriter.SetCursorPosition(22, 3);
            _consoleWriter.WriteMessage("Exchange Rate  |");
            _consoleWriter.SetCursorPosition(44, 3);
            _consoleWriter.WriteMessage("Amount      |");
            _consoleWriter.SetCursorPosition(62, 3);
            _consoleWriter.WriteMessage("Fiat (PLN)     |");
            _consoleWriter.SetCursorPosition(88, 3);
            _consoleWriter.WriteMessage("Date          |");
        }

        public void BuyCurrencies()
        {
            DisplayHeader();
            _consoleWriter.WriteMessage("Choose cryptocurrency to buy: \n");

            var i = 1;
            foreach (var currency in _exchangeRatesProvider.Currencies)
            {
                _consoleWriter.WriteMessage($"{i}. {currency.CurrencyName}\n");
                i++;
            }

            var userChosenCurrency = GetUserCurrencyChoice();

            _consoleWriter.WriteMessage($"Buying {userChosenCurrency.CurrencyName}, Last: {userChosenCurrency.Last:C}\nEnter amount to buy: ");
            var buyAmount = _validateInput.ValidateAmount();

            var userFiatBalance = GetAccountBalance();

            while (buyAmount * userChosenCurrency.Last > userFiatBalance)
            {
                _consoleWriter.WriteMessage("Not enough funds. Press ENTER to try again or press ESCAPE to go back.");

                var userPressed = _inputReader.ReadKey();

                if (userPressed.Key == ConsoleKey.Escape)
                    return;
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
                ExchangeRate = userChosenCurrency.Last,
                UserId = _showUser.ActiveUser.Id,
                Fiat = -buyAmount * userChosenCurrency.Last
            };

            _userService.RegisterTransaction(boughtCurrency);

            _consoleWriter.WriteMessage($"Bought {boughtCurrency.Amount} {boughtCurrency.CurrencyName} for {-boughtCurrency.Fiat:C}");
            _validateInput.PauseLoop();
        }

        public void SellCurrencies()
        {
            DisplayHeader();

            _consoleWriter.WriteMessage("Choose cryptocurrency to sell: \n");

            var userTrasactions = GetHistory().Where(c => c.CurrencyName == CurrencyNameEnum.Btc.ToString() || c.CurrencyName == CurrencyNameEnum.Bcc.ToString() 
                                                                            || c.CurrencyName == CurrencyNameEnum.Ltc.ToString() || c.CurrencyName == CurrencyNameEnum.Eth.ToString())
                                              .GroupBy(n => n.CurrencyName)
                                              .Select(g => g.First())
                                              .ToList();
         
            var i = 1;
            foreach (var transaction in userTrasactions)
            {
                _consoleWriter.WriteMessage($"{i}. {transaction.CurrencyName}\n");
                i++;
            }

            var userChosenCurrency = GetCurrencyFromUserTransactions(userTrasactions);
            var userChosenCurrencyBalance = _userService.UpdateUserTransactions(userChosenCurrency.CurrencyName.ToString());

            if (userChosenCurrencyBalance == 0.00)
            {
                _consoleWriter.WriteMessage($"You've sold all previously owned {userChosenCurrency.CurrencyName}. Buy some more and profit!");
                _validateInput.PauseLoop();
                return;
            }

            _consoleWriter.WriteMessage($"Selling { userChosenCurrency.CurrencyName}, last: {userChosenCurrency.Last:C}\nCurrent balance: {userChosenCurrencyBalance}\nEnter amount to sell: ");
            var sellAmount = Math.Round(_validateInput.ValidateAmount(),7);

            while (sellAmount > userChosenCurrencyBalance)
            {
                _consoleWriter.WriteMessage("Not enough coins. Press ENTER to try again or press ESCAPE to go back.");

                var userPressed = _inputReader.ReadKey();

                if (userPressed.Key == ConsoleKey.Escape)
                    return;
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
                ExchangeRate = userChosenCurrency.Last,
                UserId = _showUser.ActiveUser.Id,
                Fiat = sellAmount * userChosenCurrency.Last
            };

            _userService.RegisterTransaction(soldCurrency);

            _consoleWriter.WriteMessage($"Sold {-soldCurrency.Amount} {soldCurrency.CurrencyName} for {soldCurrency.Fiat:C}");
            _validateInput.PauseLoop();
        }

        internal Currency GetCurrencyFromUserTransactions(List<TransactionDto> userTrasactions)
        {
            TransactionDto transaction = null;
            do
            {
                var userInput = _inputReader.ReadKey();
                int.TryParse(userInput.KeyChar.ToString(), out int choice);
                if (choice != 0 && choice <= userTrasactions.Count)
                {
                    transaction = userTrasactions[choice - 1];
                }
                else
                {
                    _consoleWriter.WriteMessage("\nInvalid option, choose again.");
                }
            } while (transaction == null);

            Currency currencyExtractedFromTransaction =
                _exchangeRatesProvider.Currencies.SingleOrDefault(c =>
                    c.CurrencyName.ToString() == transaction.CurrencyName);

            return currencyExtractedFromTransaction;
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

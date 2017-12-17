﻿using System;
using System.Collections.Generic;
using Stage2HW.Business.Dtos;
using Stage2HW.Business.Services.Enums;
using Stage2HW.Cli.IoHelpers.Interfaces;

namespace Stage2HW.Cli.IoHelpers
{
    internal class ConsoleWriter : IConsoleWriter
    {
        public void WriteMessage(string message)
        {
            Console.Write(message);
        }

        public void WriteMessageInColor(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(message);
            Console.ResetColor();
        }

        public void ClearConsole()
        {
            Console.Clear();
        }

        public void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }

        public void SetColorToGreen()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public void DisplayHistoryHeader()
        {
            Console.SetCursorPosition(1, 5);
            Console.Write("Id |");
            Console.SetCursorPosition(5, 5);
            Console.Write("Currency Name |");
            Console.SetCursorPosition(22, 5);
            Console.Write("Exchange Rate  |");
            Console.SetCursorPosition(44, 5);
            Console.Write("Amount      |");
            Console.SetCursorPosition(62, 5);
            Console.Write("Fiat (PLN)     |");
            Console.SetCursorPosition(88, 5);
            Console.Write("Date          |");
        }

        public void DisplayTransactionsHistory(List<TransactionDto> transactionHistory)
        {
            int i = 7;
            foreach (var transaction in transactionHistory)
            {
                if (transaction.CurrencyName != CurrencyNameEnum.Pln.ToString())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.SetCursorPosition(2, i);
                Console.Write($"{transaction.Id}");
                Console.SetCursorPosition(10, i);
                Console.Write($"{transaction.CurrencyName}");
                if (transaction.CurrencyName == CurrencyNameEnum.Pln.ToString())
                {
                    Console.SetCursorPosition(27, i);
                    Console.Write($"n/a");
                }
                else
                {
                    Console.SetCursorPosition(24, i);
                    Console.Write($"{transaction.ExchangeRate:C}");
                }
                Console.SetCursorPosition(45, i);
                Console.Write($"{transaction.Amount}");
                Console.SetCursorPosition(62, i);
                Console.Write($"{transaction.Fiat:C}");
                Console.SetCursorPosition(80, i);
                Console.Write($"{transaction.TransactionDate}");

                Console.ResetColor();
                i++;
            }
        }
    }
}

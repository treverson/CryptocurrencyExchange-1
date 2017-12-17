using System.Collections.Generic;
using Stage2HW.Business.Dtos;

namespace Stage2HW.Cli.IoHelpers.Interfaces
{
    internal interface IConsoleWriter
    {
        void ClearConsole();
        void WriteMessage(string message);
        void WriteMessageInColor(string message);
        void SetCursorPosition(int left, int top);
        void DisplayHistoryHeader();
        void SetColorToGreen();
        void DisplayTransactionsHistory(List<TransactionDto> transactionsHistory);
    }
}
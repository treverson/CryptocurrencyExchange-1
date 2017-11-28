namespace Stage2HW.Cli.IoHelpers.Interfaces
{
    interface IConsoleWriter
    {
        void ClearConsole();
        void WriteMessage(string message);
        void SetCursorPosition(int left, int top);
    }
}
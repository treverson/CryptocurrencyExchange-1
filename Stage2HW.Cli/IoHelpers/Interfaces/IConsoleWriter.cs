namespace Stage2HW.Cli.IoHelpers.Interfaces
{
    internal interface IConsoleWriter
    {
        void ClearConsole();
        void WriteMessage(string message);
        void SetCursorPosition(int left, int top);
    }
}
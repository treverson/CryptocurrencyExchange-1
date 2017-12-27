namespace Stage2HW.Cli.Menu.Interfaces
{
    internal interface IMenu
    {
        void PrintMenu();
        void RunOption();
        void AddOptions();
        bool Exit { get; set; }
    }
}
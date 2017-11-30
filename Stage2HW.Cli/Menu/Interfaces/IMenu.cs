using Stage2HW.Business.Dtos;

namespace Stage2HW.Cli.Menu.Interfaces
{
    internal interface IMenu
    {
        void PrintMenu();
        void RunOption();
        void AddOptions();
        bool Exit { get; set; }
        UserDto ActiveUser { get; set; }
    }
}
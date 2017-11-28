using Stage2HW.Business.Dtos;
using Stage2HW.Cli.Menu.MenuOptions;

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
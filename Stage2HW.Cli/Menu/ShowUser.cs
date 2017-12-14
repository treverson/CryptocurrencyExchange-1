using Stage2HW.Business.Dtos;
using Stage2HW.Cli.IoHelpers.Interfaces;

namespace Stage2HW.Cli.Menu
{
    class ShowUser : IShowUser
    {
        public UserDto ActiveUser { get; set; }
    }
}

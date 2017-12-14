using Stage2HW.Business.Dtos;

namespace Stage2HW.Cli.IoHelpers.Interfaces
{
    public interface IShowUser
    {
        UserDto ActiveUser{ get; set; }
    }
}

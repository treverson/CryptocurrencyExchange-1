using Stage2HW.Business.Dtos;

namespace Stage2HW.Business.Services.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticatedUserDto AuthenticateUser(UserDto userDto);
    }
}
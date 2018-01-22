using AutoMapper;
using Stage2HW.Business.Dtos;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.DataAccess.Models;
using Stage2HW.DataAccess.Repositories.Interfaces;

namespace Stage2HW.Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthenticationService(IUserRepository userRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();

            });
            _mapper = config.CreateMapper();
            _userRepository = userRepository;
         
        }

        public AuthenticatedUserDto AuthenticateUser(UserDto userDto)
        {
            var userEntity = _mapper.Map<UserDto, User>(userDto);

            var userToAuthenticate = _userRepository.GetUser(userEntity.Login, userEntity.Password);

            var user = _mapper.Map<User, UserDto>(userToAuthenticate);

            if (userToAuthenticate == null)
            {
                return new AuthenticatedUserDto()
                {
                    Login = userDto.Login,
                    IsAuthenticated = false
                };
            }

            return new AuthenticatedUserDto()
            {
                UserId = user.Id,
                Login = user.Login,
                IsAuthenticated = _userRepository.CheckUserPassword(userToAuthenticate)
            };

        }
    }
}

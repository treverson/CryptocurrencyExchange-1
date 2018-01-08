using Stage2HW.Business.Dtos;
using Stage2HW.Business.Services.Interfaces;
using System.Web.Http;

namespace Stage2HW.WebApi.Controllers
{
    public class AuthenticationController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public AuthenticatedUserDto AuthenticateUser([FromBody] UserDto user)
        {
            return _authenticationService.AuthenticateUser(user);
        }
    }
}

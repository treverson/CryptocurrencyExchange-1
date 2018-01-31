using Stage2HW.Business.Dtos;
using Stage2HW.Business.Services.Interfaces;
using System.Web.Http;

namespace Stage2HW.WebApi.Controllers
{
    public class RegistrationController : ApiController
    {
        private readonly IUserService _userService;

        public RegistrationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public void RegisterUser(UserDto user)
        {
            _userService.AddUser(user);
        }
    }
}

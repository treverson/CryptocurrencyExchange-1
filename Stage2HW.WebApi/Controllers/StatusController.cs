using System.Web.Http;

namespace Stage2HW.WebApi.Controllers
{
    public class StatusController : ApiController
    {
        public string Get()
        {
            return "Currency exchange WebApi running";
        }
    }
}

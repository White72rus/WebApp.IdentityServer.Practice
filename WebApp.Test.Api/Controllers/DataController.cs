using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Test.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [Authorize]
        [Route("[action]")]
        public string GetData()
        {
            return "Data from API server";
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApp.IdentityServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetDataAsync() {

            var httpClient = _httpClientFactory.CreateClient();

            var httpResponse = await httpClient.GetStringAsync("https://localhost:9001/api/data/getdata/");

            ViewBag.Message = httpResponse;

            return View();
        }
    }
}

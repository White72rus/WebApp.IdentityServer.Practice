using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApp.IdentityServer.Controllers
{
    [Route("[controller]")]
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

        [Route("[action]")]
        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        [Route("[action]")]
        public async Task<IActionResult> GetDataAsync() {

            var identityServerClient = _httpClientFactory.CreateClient();
            var discoveryDocument = await identityServerClient.GetDiscoveryDocumentAsync("https://localhost:5001");
            var token = await identityServerClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint,
                    ClientId = "smsis_portal_web",
                    ClientSecret = "smsis_portal_web",
                    Scope = "smsis.portal",
                });


            var httpClient = _httpClientFactory.CreateClient();
            httpClient.SetBearerToken(token.AccessToken);

            var httpResponse = await httpClient.GetAsync("https://localhost:9001/api/data/getdata/");

            if (!httpResponse.IsSuccessStatusCode)
            {
                ViewBag.Message = httpResponse.StatusCode.ToString();
            }
            else
            {
                ViewBag.Message = await httpResponse.Content.ReadAsStringAsync();
            }

            return View();
        }
    }
}

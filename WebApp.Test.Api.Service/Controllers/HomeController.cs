using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Test.Api.Service.ViewModel;

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
        [Authorize(Roles = "User")]
        public IActionResult Secret()
        {
            return View();
        }

        [Route("[action]")]
        //[Authorize]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Admin()
        {
            //var jsonToken = await HttpContext.GetTokenAsync("access_token");

            //var token = new JwtSecurityTokenHandler().ReadToken(jsonToken) as JwtSecurityToken;

            return View();
        }

        [Route("[action]")]
        [Authorize]
        public async Task<IActionResult> Claims()
        {
            var jsonTokenAccess = await HttpContext.GetTokenAsync("access_token");
            var jsonTokenId = await HttpContext.GetTokenAsync("id_token");

            //var token = new JwtSecurityTokenHandler().ReadToken(jsonToken) as JwtSecurityToken;

            var userClaims = User.Claims;

            List<ClaimsView> claims = new List<ClaimsView>();
            claims.Add(new ClaimsView("Id token", jsonTokenId));
            claims.Add(new ClaimsView("Access token", jsonTokenAccess));
            claims.Add(new ClaimsView("User claims", userClaims));

            ViewBag.Claims = claims;

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

        [Route("[action]")]
        public IActionResult AccessDeniedMy()
        {
            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Test.Api.Service.ViewModel
{
    public class ClaimsView
    {
        public List<Claim> Claims { get; set; }
        public string Name { get; set; } = "N/A";
        public string Token { get; set; } = "N/A";

        public ClaimsView(string name, IEnumerable<Claim> claims)
        {
            Claims = claims.ToList() ?? throw new ArgumentNullException(nameof(claims));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public ClaimsView(string name, string token)
        {
            Token = token ?? throw new ArgumentNullException(nameof(token));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Claims = (new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken).Claims?.ToList();
        }
    }
}

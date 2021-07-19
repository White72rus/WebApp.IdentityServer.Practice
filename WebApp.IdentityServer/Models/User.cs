using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.IdentityServer.Models
{
    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }
    }
}

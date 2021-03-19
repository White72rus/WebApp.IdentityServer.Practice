using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.IdentityServer.DataLayer.Entityes
{
    public class User
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("NAME", TypeName = "nvarchar(128)")]
        public string Name { get; set; }

        [Column("ROLE", TypeName = "nvarchar(128)")]
        public string Role { get; set; }

        [Column("PASS", TypeName = "nvarchar(256)")]
        public string Password { get; set; }

        [Column("LOGIN", TypeName = "nvarchar(128)")]
        public string Login { get; set; }

        [Column("EMAIL", TypeName = "nvarchar(128)")]
        public string Email { get; set; }
    }
}

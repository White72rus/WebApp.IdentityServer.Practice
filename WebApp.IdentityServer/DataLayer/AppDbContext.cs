using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp.IdentityServer.DataLayer
{
    public class AppDbContext : IdentityDbContext
    {
        
        //public DbSet<User> Users;

        //public AppDbContext()
        //{
        //    Database.EnsureCreated();
        //}

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
    }
}

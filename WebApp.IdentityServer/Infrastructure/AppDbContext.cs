using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.IdentityServer.Models;

namespace WebApp.IdentityServer.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users;
    }
}

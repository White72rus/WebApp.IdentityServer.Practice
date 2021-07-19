using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.IdentityServer.Models;

namespace WebApp.IdentityServer.Infrastructure
{
    public class UserService
    {

        private User _user = new User();

        public void Add(User user)
        {
            _user = user;
        }

        public User Get() => _user;
    }
}

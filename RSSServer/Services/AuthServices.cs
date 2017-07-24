using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSSServer.Models;
using System.Security.Claims;

namespace RSSServer.Services
{
    public class AuthServices
    {
        private DataContext context;

        public AuthServices()
        {
            context = new DataContext();
        }

        public ClaimsIdentity UserVerification (string Login, string Password)
        {
            User person = context.Users.FirstOrDefault(x => x.Login == Login && x.Password == Password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Password)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }

        public bool CreateUser(string Login, string Password)
        {
            User user = new User() { Login = Login, Password = Password };
            context.Users.Add(user);
            return context.SaveChanges() > 0;
        }
    }
}

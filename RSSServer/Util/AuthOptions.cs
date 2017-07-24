using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace RSSServer.Util
{
    public class AuthOptions
    {
        public const string ISSUER = "AuthServer";
        public const string AUDIENCE = "http://localhost:51884";
        const string KEY = "KodisoftKodisoft";
        public const int LIFETIME = 1440;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

    }
}

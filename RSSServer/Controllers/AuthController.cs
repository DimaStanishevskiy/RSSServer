using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSSServer.Services;
using System.IdentityModel.Tokens.Jwt;
using RSSServer.Util;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using RSSServer.Models;

namespace RSSServer.Controllers
{
    [Produces("application/json")]
    [Route("api/login")]
    public class AuthController : Controller
    {
        private AuthServices authServices = new AuthServices();

        [HttpPost, Route("token")]
        public string Token([FromBody]User user)
        {
            var identity = authServices.UserVerification(user.Login, user.Password);
            if (identity == null)
            {
                Response.StatusCode = 400;
                return null;
            }
            
            var now = DateTime.UtcNow;
            // JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            Response.StatusCode = 200;
            return encodedJwt;
        }
        [HttpPost, Route("registration")]
        public IActionResult Registration([FromBody]User user)
        {
            if (authServices.CreateUser(user.Login, user.Password))
                return new OkResult();
            else
                return new BadRequestResult();
            
        }
    }
}
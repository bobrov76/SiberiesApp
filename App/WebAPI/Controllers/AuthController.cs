using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.HelpClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {

        private readonly testDbContext _context;

        public AuthController(testDbContext context)
        {
            _context = context;
        }


        [HttpPost("/token")]
        public ActionResult<Employees> PostEmployees(Employees employees)
        {
            var username = employees.Username;
            var password = employees.Password;
            if (username == null || password == null) return BadRequest("Invalid data");

            Employees person = _context.Employees.FirstOrDefault(x => x.Username == username && x.Password == password);
            if (person == null) return BadRequest("Invalid data");

            var identity = GetIdentity(person);

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                accessToken = encodedJwt,
                username = identity.Name,
                rol = person.AuthRol
            };

            return Ok(response);
        }

        private ClaimsIdentity GetIdentity(Employees person)
        {
            var claims = new List<Claim>
           {
              new Claim(ClaimsIdentity.DefaultNameClaimType, person.Username),
              new Claim(ClaimsIdentity.DefaultRoleClaimType, person.AuthRol)
           };

            ClaimsIdentity claimsIdentity =
                 new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                 ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}


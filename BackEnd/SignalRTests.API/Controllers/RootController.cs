using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SignalRTests.API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SignalRTests.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RootController : ControllerBase
    {
        private string[] _cities = new string[] { "Blumenau", "New York", "London" };

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        [Route("login")]
        public IActionResult Login([FromBody] LoginForm form, [FromServices] IConfiguration configuration)
        {
            var rng = new Random();
            var city = _cities[rng.Next(0, (_cities.Length - 1))];

            var expirationDate = DateTime.UtcNow.AddMinutes(600);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["TokenConfig:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("username", form.UserName, ClaimValueTypes.String),
                    new Claim("city", city, ClaimValueTypes.String)
                }),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(tokenHandler.WriteToken(token));
        }
    }
}

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CMSiAPI.Controllers
{
    [Route("/token")]
    public class TokenController : Controller
    {
        [HttpPost]
        public IActionResult Create(string username, string password)
        {
            if (IsValidUsernameAndPasswordCombination(username, password))
            {
                return new ObjectResult(GenerateToken(username));
            }
            return BadRequest();
        }

        private bool IsValidUsernameAndPasswordCombination(string username, string password)
        {
            // Authentication logic here
            return !string.IsNullOrEmpty(username) && username == password;
        }

        private string GenerateToken(string username)
        {
            var claims = new Claim[] {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };
            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Your secret here over 16 characeters long for HmacSha256")),
                                             SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
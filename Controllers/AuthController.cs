using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BoletosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("token")]
        public IActionResult Token()
        {
            // Demo token generator - in real apps validate user credentials
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("very_secret_key_for_testing_purposes_only"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: new[] { new Claim(ClaimTypes.Name, "candidate") },
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}

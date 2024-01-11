using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BL;
using DL;
using Microsoft.AspNetCore.Identity;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly string secretKey;

        public AuthenticationController(IConfiguration configuracion)
        {
            secretKey = configuracion.GetSection("settings").GetSection("secreKey").ToString(); 
        }

        [HttpPost]
        [Route("validar")]
        public IActionResult Validar([FromBody] AspNetUser user)
        {
            if(user.Email == "" && user.PasswordHash == "")
            {
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();

                // ORIGINAL:
                // claims.AddClaims(new Claim(ClaimTypes.NameIdentifier, request.Email));

                _ = new List<Claim> {  // Prueba 
                    new Claim(ClaimTypes.Email, user.Email)                   
                    };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(1),   // Tiempo en que expira el token 
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHeadler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHeadler.CreateToken(tokenDescriptor);

                string tokenCreado = tokenHeadler.WriteToken(tokenConfig);

                return StatusCode(StatusCodes.Status200OK, new { token = tokenCreado });
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });
            }
            
        }
    }
}

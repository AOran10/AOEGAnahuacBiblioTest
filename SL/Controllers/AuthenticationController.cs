using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BL;
using DL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]  // Todos los métodos del controlador
    public class AuthenticationController : ControllerBase
    {
        private readonly string secretKey;

        //inyección de dependencias
        public AuthenticationController(IConfiguration configuracion)   // Acceso a AppSettings
        {
            secretKey = configuracion.GetSection("settings").GetSection("secretKey").ToString(); // Acceso a secretKey
        }

        [HttpPost]
        [Route("Validar")]
        public IActionResult Validar(ML.IdentityUser request)
        {
            if (request.UserName == "calitimo55@gmail.com")
            {
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.Email));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokenCreado = tokenHandler.WriteToken(tokenConfig);

                return StatusCode(StatusCodes.Status200OK, new { token = tokenCreado });
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" , message = "La autentificación ha fallado"} );
            }
        }
    }
}

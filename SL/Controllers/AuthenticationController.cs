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
using ML;

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

            ML.Result result = BL.IdentityUser.ConfirmPassword(request);

            if (result.Correct)
            {
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.Email));//aqui tambien venía la contraseña sin hashear

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    NotBefore = DateTime.Now,
                    Expires = DateTime.Now.AddMinutes(10),
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

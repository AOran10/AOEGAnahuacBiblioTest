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
    [Authorize]  // Todos los métodos del controlador
    public class AuthenticationController : ControllerBase
    {
        private readonly string secretKey;

        public AuthenticationController(IConfiguration configuracion)   // Acceso a AppSettings
        {
            secretKey = configuracion.GetSection("settings").GetSection("secretKey").ToString(); // Acceso a secretKey
        }

        [HttpPost]
        [Authorize]
        [Route("validar")]
        public static string GenerateTokenJwt(string UserName, string Password)
        {    
            var secretKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(UserName));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);

            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, UserName) });
            

            // create token to the user
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(              
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }
    }
}

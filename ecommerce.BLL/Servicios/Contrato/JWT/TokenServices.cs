using ecommerce.MODEL;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ecommerce.BLL.Servicios.Contrato.JWT
{
    public class TokenServices : ITokenService
    {
        private readonly JwtSettings jwtSettings;

        public TokenServices(IOptions<JwtSettings> jwtSettings)
        {
            this.jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(User user)
        {
            if (string.IsNullOrWhiteSpace(jwtSettings.Secret))
            {
                throw new InvalidOperationException("La clave secreta JWT no está configurada.");
            }

            var claims = new[]
                {
                  new Claim(ClaimTypes.Name, user.Login),
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null, // Opcional
                audience: null, // Opcional
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1), // Expira en 1 día
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

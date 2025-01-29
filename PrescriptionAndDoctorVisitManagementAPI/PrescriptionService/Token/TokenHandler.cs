using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PrescriptionService.Token
{
    public class TokenHandler
    {
        public static Token CreateToken(IConfiguration configuration,Guid pharmacyId)
        {
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(double.Parse(configuration["Jwt:Expiration"]));

            var claims = new[]
           {
            new Claim(JwtRegisteredClaimNames.Sub, pharmacyId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.DateTime)
            };

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                expires: expiration,
                signingCredentials: credentials);

            return new Token
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}

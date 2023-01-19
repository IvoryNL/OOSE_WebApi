using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebAPI.Entities;
using WebAPI.Models;

namespace WebAPI.Helpers
{
    public class AuthenticationHelper
    {
        public static HashedPasswordModel CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                return new HashedPasswordModel
                {
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                    PasswordSalt = hmac.Key
                };
            }
        }

        public static bool VerifyPasswordHash(string password, HashedPasswordModel hashedPassword)
        {
            using (var hmac = new HMACSHA512(hashedPassword.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(hashedPassword.PasswordHash);
            }
        }

        public static string CreateToken(Gebruiker user, string tokenKey)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Rol!.Naam)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: signingCredentials);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Auth
{
    public static class JwtAuthenticationManager
    {
        //key declaration
        //private readonly IConfiguration _configuration;

        //private readonly IDictionary<string, string> users = new Dictionary<string, string>
        //{ {"test", "password"}, {"test1", "password1"}, {"user", "assword"} };

        //public JwtAuthenticationManager(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        //public string? Authenticate(string username, string password)
        //{
        //    //auth failed - creds incorrect
        //    if (!users.Any(u => u.Key == username && u.Value == password))
        //    {
        //        return null;
        //    }
        //    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        //    var tokenKey = Encoding.ASCII.GetBytes(_configuration["Jwt:Token"]);
        //    SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, username)
        //        }),
        //        // Duration of the Token
        //        // Now the the Duration to 1 Hour
        //        Expires = DateTime.UtcNow.AddHours(12),

        //        SigningCredentials = new SigningCredentials(
        //            new SymmetricSecurityKey(tokenKey),
        //            SecurityAlgorithms.HmacSha256Signature) //setting sha256 algorithm
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}

        public static string DecodeJWT(string secret, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var claims = jwtToken.Claims;
            string uniqueName = jwtToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
            return uniqueName;
        }
    }
}

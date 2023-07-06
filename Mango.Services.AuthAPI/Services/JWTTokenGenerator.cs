using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Services.IServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mango.Services.AuthAPI.Services
{
    public class JWTTokenGenerator : IJWTTokenGenerator
    {
        //we have added the configuration of getting the secret string as well as issuer and audience 
        //from our appsettings.json that we added inside to be used in Token generaing
        //we are going to retrieve it here using DI

        private readonly JwtOptions _jwtOptions;
        public JWTTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            //here _jwtOptions will have all the key string that we added in our appSettings.json for JwtOptions
            _jwtOptions = jwtOptions.Value;
        }
        public string GenerateToken(ApplicationUser appUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
            var claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub , appUser.Id),
                new Claim(JwtRegisteredClaimNames.Name, appUser.UserName.ToString()),
            };

            //tokenDescripter will have config for tour token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

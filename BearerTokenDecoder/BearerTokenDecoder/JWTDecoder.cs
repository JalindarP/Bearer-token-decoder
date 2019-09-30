using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BearerTokenDecoder
{
    public sealed class JwtDecoder
    {

        public Task<string> Decode(string tokenString)
        {
            if (string.IsNullOrEmpty(tokenString))
                return Task.FromResult(string.Empty);

            var token = new JwtSecurityToken(jwtEncodedString: tokenString);
            var userId = token.Claims.First(c => c.Type == "Id").Value;

            return Task.FromResult(userId);
        }

        public string Encode( string id, string firstName, string lastName, string languageIsoCode, string email, string userName)
        {
            //var claims = new Claim[]
            //{
            //    new Claim("Id", "2D355330-B65E-4DE7-8638-D64CDD28746C"),
            //    new Claim("FirstName", "3B39146"),
            //    new Claim("LastName", "3B39146"),
            //    new Claim("LanguageIsoCode", "en"),
            //    new Claim("Email", "uzache@eurofins.com"),
            //    new Claim("UserName", "uzache")
            //};

            //var token = new JwtSecurityToken(claims: claims, audience: "http://localhost:8011/", issuer: "Online JWT Builder");


            // Define const Key this should be private secret key  stored in some safe place
            string key = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";

           // Create Security key  using private key above:
           // not that latest version of JWT using Microsoft namespace instead of System
            var securityKey = new Microsoft
               .IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            // Also note that securityKey length should be >256b
            // so you have to make sure that your private key has a proper length
            //
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials
                              (securityKey, SecurityAlgorithms.HmacSha256Signature);

            //  Finally create a Token
            var header = new JwtHeader(credentials);

            //Some PayLoad that contain information about the  customer
            var payload = new JwtPayload
           {
               { "Id", id},
               { "FirstName", firstName},
               { "LastName", lastName},
               { "LanguageIsoCode", languageIsoCode},
               { "Email", email},
               { "UserName", userName},
           };

            //
            var token = new JwtSecurityToken(header, payload);

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.WriteToken(token);
            return jwt;
        }
    }
}

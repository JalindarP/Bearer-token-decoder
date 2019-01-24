using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

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

        public string Encode( string id, string firstName, string LastName, string languageIsoCode, string email, string userName)
        {
            var claims = new Claim[]
            {
                new Claim("Id", "2D355330-B65E-4DE7-8638-D64CDD28746C"),
                new Claim("FirstName", "3B39146"),
                new Claim("LastName", "3B39146"),
                new Claim("LanguageIsoCode", "en"),
                new Claim("Email", "uzache@eurofins.com"),
                new Claim("UserName", "uzache")
            };

            var token = new JwtSecurityToken(claims: claims, audience: "http://localhost:8011/", issuer: "Online JWT Builder");
        
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.WriteToken(token);
            return jwt;
        }
    }
}

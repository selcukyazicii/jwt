using jwt.Abstract;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace jwt.Concrete
{
    public class JwtAuthenticationManager : IJWTAuthenticationManager
    {
       
        public JwtAuthenticationManager()
        {

        }
        private readonly string _key;
        public JwtAuthenticationManager(string key)
        {
            _key = key;
        }
        private readonly IDictionary<string, string> users = new Dictionary<string, string>
        {
            {"selo","123" },
            {"ekrem","456" }
        };
        public string Authenticate(string userName, string password)
        {
            if (!users.Any(x=>x.Key==userName &&x.Value==password))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("bubenimuzunstringkeydeğerim");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

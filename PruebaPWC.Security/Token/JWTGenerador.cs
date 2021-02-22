using Microsoft.IdentityModel.Tokens;
using PruebaPWC.Application.Contracts;
using PruebaPWC.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PruebaPWC.Security.Token
{
    public class JWTGenerador : IJWT
    {
        public string CrearToken(Usuario usuario, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName)
            };

            if (roles != null)
            {
                foreach (var rol in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authnetication"));
            var credencials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(2),
                SigningCredentials = credencials
            };

            var tokenHanddler = new JwtSecurityTokenHandler();
            var token = tokenHanddler.CreateToken(tokenDescription);

            return tokenHanddler.WriteToken(token);

        }



    }
}

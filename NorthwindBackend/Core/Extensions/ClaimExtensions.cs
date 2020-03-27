using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Extensions
{
    public static class ClaimsExtensions
    {
        public static void AddEmail(this ICollection<Claim> claims, string email) // this ICollection<Claim> claims ile metodun neyi extend edeceğini belirtmiş olduk. 
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, value: email));
        }
        public static void AddName(this ICollection<Claim> claims, string name) // this ICollection<Claim> claims ile metodun neyi extend edeceğini belirtmiş olduk. 
        {
            claims.Add(new Claim(ClaimTypes.Name, value: name));
        }

        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier) // this ICollection<Claim> claims ile metodun neyi extend edeceğini belirtmiş olduk. 
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, value: nameIdentifier));
        }
        public static void AddRoles(this ICollection<Claim> claims, string[] roles) // this ICollection<Claim> claims ile metodun neyi extend edeceğini belirtmiş olduk. 
        {
            // role bir dizi şeklinde gelecektir.
            roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, value: role)));
        }
    }
}
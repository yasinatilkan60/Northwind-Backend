using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encyription
{
    public class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            // security key ve bir algoritmadan oluşan nesne geri dönecektir.
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature); 
        }
    }
}

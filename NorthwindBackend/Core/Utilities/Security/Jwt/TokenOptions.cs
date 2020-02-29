using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Jwt
{
    public class TokenOptions // Bu TokenOptions api içerisinde appsetting.json içerisinde map edilmiş nesne olarak kullanılacaktır. Daha nesnel bir çalışma için.
    {
        public string Audience { get; set; } // Token Kullanıcısı
        public string Issuer { get; set; } // Token İmzası
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; } 
    }
}

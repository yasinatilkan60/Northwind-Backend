using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Jwt
{
    // AccessToken erişim için kullanılacak olan anahtardır.
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime  Expiration { get; set; } // Token'ın ne kadar geçerli olduğunu gösterir.

        // Daha sonra refresh token sistemi de kullanılabilir.
    }
}

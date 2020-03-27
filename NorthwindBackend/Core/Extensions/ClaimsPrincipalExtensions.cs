using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Linq;

namespace Core.Extensions
{
    // Extension yazılacağı için public static yazılmıştır.
    public static class ClaimsPrincipalExtensions
    {

        // Buradaki yaklaşım Claims Principal'ın (mevcut kullanıcı) sadece rollerine değil başka bilgilerine de erişmek üzerine kurulacaktır.
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            // this ClaimsPrincipal claimsPrincipal extend edilecek ve claimType için filtreleme yapılacak.
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList(); // her bir claim type'ın key ve value değeri vardır. 
            return result;
        }
        
        // Rolleri döndürelim.
        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal) // claims principal extend edilir.
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }
        //Artık kullanıcı ClaimsPrincipalExtensions.ClaimsRoles dediğinde roller gelecektir.
    }
}

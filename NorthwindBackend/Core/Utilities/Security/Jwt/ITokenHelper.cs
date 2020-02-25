using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Jwt
{
    // Token üretimini gerçekleştirecek bir helper. Interface ile ilerleyen zamanlarda Jwt altyapısının dışına çıkarmak mümkündür.
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims); // Kullanıcı bilgileri ve rollerinin de token'a eklendi.
    }
}

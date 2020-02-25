using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IUserDal: IEntityRepository<User>
    {
        // Ek olarak getirilecek operasyon;
        List<OperationClaim> GetClaims(User user); // Kullanıcı verildiğinde onun rollerini çekeceğiz.
    }
}

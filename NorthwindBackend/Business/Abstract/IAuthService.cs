using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        // Bu servis ile sisteme login veya register olacağım.
        // Interface içerisinde gerekli operasyonlar vardır.

        // Kullanıcı sisteme kayıt olduğunda ona bir result verilir.
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExist(string email); // Kullanıcı var mı?
        IDataResult<AccessToken> CreateAccessToken(User user); 

        // Concrete tarafında bu interface için bir manager yazılacaktır.
    }
}

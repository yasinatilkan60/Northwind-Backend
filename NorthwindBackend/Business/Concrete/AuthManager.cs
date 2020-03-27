using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService; // İşlemleri gerçekleştirmek için bu nesneyi kullanmalıyız.
        ITokenHelper _tokenHelper; // Kullanıcı login olduğunda bir token vereceğiz.

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }
        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims); // claim kişinin rolleridir.
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);// mevcut kullanıcıyı getirelim.
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            // Eğer kllanıcı var ise kullanıcıdan gelen açık şifreyi salt yaparak, veritabanındaki ilgili hashi mevcut hash ile karşılaştırırız.
            // kodu burada salt ile beraber hash'e çevirebilirim ama başka bir yerde de kullanma ihtimaline karşı bunu HashHelper yazacağız.
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SucccesfulLogin);
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            // İlk önce UserExist ile kullanıcı daha önceden kayıt yaptırmış mı onu kontrol edelim.
            // HashHelper ile hash ve salt'ın bize dönecektir.
            byte[] passwordHash, passwordSalt; // passwordhash ve passwordsalt boş gönderilecek.
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt); // boş gönderilen salt ve hash doldurulup geri döndürülecektir.
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true // Kullanıcının aktiflik durumu.
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IResult UserExist(string email) // Kullanıcının sistemde olup olmadığını kontrol etmek için kullanılır.
        {
            if (_userService.GetByMail(email) != null) // kullanıcı varsa.
            {
                return new ErrorResult(Messages.UserAlreadyExists); // Kullanıcı zaten var ise.
            }
            return new SuccessResult();
        }
    }
}
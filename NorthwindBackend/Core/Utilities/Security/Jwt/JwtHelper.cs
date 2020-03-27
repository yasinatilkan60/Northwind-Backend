using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encyption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        // Yapılacak işlem; appsetting.json (konfigürasyon dosyasından) token'ı okumak olacak.
        public IConfiguration Configuration { get; }// IConfiguration ile aynı konfigürasyonu yarın başka bir apiden de kullanabilirim. 
        private TokenOptions _tokenOptions; // appsetting.json içerisinden gelecek TokenOptions alanını burdaki nesneye aktaracağız.
        DateTime _accessTokenExpiration; // Bunu da ctorda set edeceğiz çünkü her yerde kullacağız.
        public JwtHelper(IConfiguration configuration) // Api tarafında bu konfigürasyonun tanımlanması gerekmektedir. (Startup.cs ve appsettings.json)
        {
            Configuration = configuration; // appsetting.json içerisinden gelecek veriyi burada configuration ile okuyacağız.
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>(); // Artık elimizde Audience, Issuer vs gibi alanların olduğu bir token nesnesi vardır.
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration); // Artık elimizde dakika değil bir tarih vardır.
        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            // Token'ı oluştururken;
            // var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey)); // Bu kod standart bir kod olduğundan her seferinde aynı işlemi yapmak istemiyoruz.
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);

            // Bir sonraki aşama signing  credential yapısını oluşturacağız. Bunun için yine bir helper yazacagız. !
            // Signing  credential; bizim security key ve algoritmamızı belirlemiş olduğumuz nesnedir.
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);

            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
            // Token muvcut ama elimizdeki tokenı handler ile yazmalıyız.
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt); // WriteToken ile token string'e çevrilmiştir.

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        // İhtiyacamız olan bilgiler token bilgileri (_tokenOptions), Kullanıcı bilgilerini, signingCredentials bilgilerini ve kullanıcı rollerini
        // kullanarak bir adet token oluşturacağız.
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials,
            List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                    issuer: tokenOptions.Issuer,
                    audience: tokenOptions.Audience,
                    expires: _accessTokenExpiration,
                    notBefore: DateTime.Now, // Token'ın expiration bilgisi şu andan önce ise geçerli olmayacaktır.
                    claims: SetClaims(user, operationClaims), //claims: operationClaims, // Bir claim isteniyor.
                    signingCredentials: signingCredentials
                );
            return jwt;
        }

        // Claim set edeceğimiz bir yapı.
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            // claims.AddEmail() vs kullanabilmek için claim nesnesini extend etmeliyim. Yani bir extensions yazacağız.
            // claims.Add(new Claim("email", user.Email));  // Kurumsal mimaride bu tarz kullanımlardan kaçınmalıyız. (Magic string)

            // Extension yazmak ileri c# tekniğidir. Extension ile bir class'ı genişletiriz.
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray()); // operationClaims.Select(c => c.Name).ToArray() ile dizi şeklinde extension'a gönderim yapıldı.
            return claims;
        }

        public AccessToken CreateToken(User user)
        {
            throw new NotImplementedException();
        }
    }
}
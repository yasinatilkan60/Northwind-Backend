using Business.Abstract;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] // api/auth
    [ApiController]
    public class AuthController : Controller
    {
        // Controller'ı class olarak da oluşturabiliriz.
        private IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")] // Kullanıcıdan gelen bilgi ile login işlemi gerçekleştireceğiz.
        public ActionResult Login(UserForLoginDto userForLoginDto) 
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }
            // Kullanıcı login oldu ise,
            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data); // kullanıcıya data yani token verilir.
            }
            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            // Kullanıcı var mı ?
            var userExist = _authService.UserExist(userForRegisterDto.Email);
            if (!userExist.Success) // amaç gelen sonuca göre request'in ne olduğunu öğrenmek.
            {
                return BadRequest(userExist.Message);
            }
            var registerResult = _authService.Register(userForRegisterDto,userForRegisterDto.Password); // aslında ayrıca göndermeye gerek yoktu.
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}

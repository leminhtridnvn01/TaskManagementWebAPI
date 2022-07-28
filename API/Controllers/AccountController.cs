using API.Services;
using Domain.DTOs.Users;
using Domain.Interfaces.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagement.ApplicationTier.API.Controllers;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly EFContext _context;

        public AccountController(EFContext context)
        {
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> RegisterAsync([FromServices] IUserService _userService, [FromServices] ITokenService _tokenService, RegisterRequest registerDto)
        {
            var newUser = await _userService.CreateUser(registerDto);
            if (newUser == null)
            {
                return BadRequest("Can not register now!");
            }
            return Ok("Welcome " + newUser.UserName + " !!!");
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromServices] IUserService _userService, [FromServices] ITokenService _tokenService, LoginRequest loginRequest)
        {
            var user = await _userService.Login(loginRequest);
            if (await _userService.Login(loginRequest) == null)
            {
                return Unauthorized("Invalid Username or Password");
            }
            return Ok(new AccountResponse
            {
                Username = loginRequest.UserName,
                Token = _tokenService.CreateToken(user)
            });
        }
    }
}
using AutoMapper;
using Domain.DTOs.Users;
using Domain.DTOs.Users.UpdateUser;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagement.ApplicationTier.API.Controllers;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<UserDetailResponse>> Get([FromRoute] int userId)
        {
            var member = await _userService.GetUser(userId);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<UpdateUserResponse>> Update([FromBody] UpdateUserRequest userInput)
        {
            var newUser = await _userService.UpdateUser(userInput);
            if (newUser == null)
            {
                return NotFound();
            }
            return Ok(newUser);
        }

        [Authorize]
        [HttpPatch]
        public async Task<ActionResult<UpdateUserResponse>> ChangeEmail([FromBody] string newEmail)
        {
            var newUser = await _userService.ChangeEmail(newEmail);
            if (newUser == null)
            {
                return NotFound();
            }
            return Ok(newUser);
        }
    }
}
using System;
using System.Threading.Tasks;
using Common.DTO;
using Common.Services.Infrastructure.Services;
using Common.WebApiCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Common.WebApiCore.Controllers
{
    [Route("users")]
    public class UsersController : BaseApiController
    {
        protected readonly IUserService userService;
        protected readonly JwtManager jwtManager;
        public UsersController(IUserService userService, JwtManager jwtManager)
        {
            this.userService = userService;
            this.jwtManager = jwtManager;
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await userService.GetById(id);
            return Ok(user);
        }

        [HttpGet]
        [Route("current")]
        public async Task<IActionResult> GetCurrent()
        {
            var currentUserId = User.GetUserId();
            if (currentUserId != Guid.Empty)
            {
                var user = await userService.GetById(currentUserId);
                return Ok(user);
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create(UserDTO userDto)
        {
            if (userDto.Id != Guid.Empty)
            {
                return BadRequest();
            }

            var result = await userService.Edit(userDto);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Edit(Guid id, UserDTO userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }

            var result = await userService.Edit(userDto);
            return Ok(result);
        }

        [HttpPut]
        [Route("current")]
        public async Task<IActionResult> EditCurrent(UserDTO userDto)
        {
            var currentUserId = User.GetUserId();
            if (currentUserId != userDto.Id)
            {
                return BadRequest();
            }

            var user = await userService.GetById(userDto.Id);
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.TimeZoneId = userDto.TimeZoneId;
            var result = await userService.Edit(user);
            return Ok(result);

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await userService.Delete(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("{userId:int}/photo")]
        public async Task<IActionResult> UserPhoto(Guid userId, string token)
        {
            var user = jwtManager.GetPrincipal(token);
            if (user == null || !user.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var photoContent = await userService.GetUserPhoto(userId);

            if (photoContent == null)
            {
                return NoContent();
            }

            return File(photoContent, contentType: "image/png");
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var users = await userService.GetAll();
            return Ok(users);
        }
    }
}
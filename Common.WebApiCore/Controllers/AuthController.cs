using System.Threading.Tasks;
using Common.DTO.AuthDTO;
using Common.Services.Infrastructure.Services;
using Common.WebApiCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Common.WebApiCore.Controllers
{
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        protected readonly IAuthenticationService authService;
        private readonly IUserService _userService;

        public AuthController(IAuthenticationService authService, IUserService userService)
        {
            this.authService = authService;
            this._userService = userService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var result = await authService.Login(loginDto);

            if (result.Succeeded)
            {
                return Ok(new { token = result.Data });
            }
            if (result.IsModelValid)
            {
                return Unauthorized();
            }

            return BadRequest();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp(SignUpDTO signUpDto)
        {
            var user = await _userService.GetByEmail(signUpDto.Email);
            if (user != null)
            {
                return BadRequest("A user with such an email already exists.");
            }

            var result = await authService.SignUp(signUpDto);

            if (result.Succeeded)
                return Ok(new { token = result.Data });

            if (result.IsModelValid)
                return Unauthorized();

            return BadRequest();
        }
    }
}
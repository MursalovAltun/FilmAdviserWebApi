using System.Threading.Tasks;
using Common.DTO.AuthDTO;
using Common.Services.Infrastructure.Services;
using Common.WebApiCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Common.WebApiCore.Controllers
{
    /// <summary>
    /// Controller with endpoints that allows to manage user authentication
    /// </summary>
    [Route("auth")]
    public class AuthController : BaseApiController
    {
        protected readonly IAuthenticationService authService;
        private readonly IUserService _userService;

        public AuthController(IAuthenticationService authService, IUserService userService)
        {
            this.authService = authService;
            this._userService = userService;
        }

        /// <summary>
        /// This endpoint is using for sign in using email and password.
        /// </summary>
        /// <param name="loginDto">DTO that should pass for sign in</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /auth/sign-in
        ///     {
        ///         "email": "example@example.com",
        ///         "password": "Uj8P_1QQm"
        ///     }
        /// </remarks>
        /// <returns>Token DTO</returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Token), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (!ModelState.IsValid) return BadRequest();

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

        /// <summary>
        /// This endpoint is using for register new user.
        /// If user with provided email is exists it will return BadRequest.
        /// </summary>
        /// <param name="signUpDto">DTO that should pass for register new user</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /auth/sign-up
        ///     {
        ///         "email": "example@example.com",
        ///         "password": "Uj8P_1QQm",
        ///         "confirmPassword": "Uj8P_1QQm",
        ///         "fullName": "Altun Mursalov",
        ///         "timeZoneId": "Eastern Standard Time"
        ///     }
        /// </remarks>
        /// <returns>Token DTO</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("sign-up")]
        [ProducesResponseType(typeof(Token), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUp(SignUpDTO signUpDto)
        {
            if (!ModelState.IsValid) return BadRequest();

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
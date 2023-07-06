using Mango.Services.AuthAPI.IServices;
using Mango.Services.AuthAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected APIResponseDTO _response;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO regReqDTO)
        {
            var errorMessage = await _authService.Register(regReqDTO);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                //means there is some eero
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO loginReqDTO)
        {
            var loginRepsponse = await _authService.Login(loginReqDTO);
            if (loginRepsponse.User == null)
            {
                //incase the user credential validaton failed
                _response.IsSuccess = false;
                _response.Message = "Username or Password is incorrect!";
                return BadRequest(_response);
            }

            _response.Result = loginRepsponse;
            return Ok(_response);
        }



        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var asssigningRole = await _authService.AssignRole(registerRequestDTO.Email, registerRequestDTO.Role.ToUpper());
            if (!asssigningRole)
            {
                //incase the user credential validaton failed
                _response.IsSuccess = false;
                _response.Message = "Error encountered while assigning Role!!";
                return BadRequest(_response);
            }

            return Ok(_response);
        }

    }
}

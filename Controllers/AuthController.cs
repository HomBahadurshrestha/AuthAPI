using AuthAPI.Models;
using AuthAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    // Defines the base URL route for this controller 
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // used to handle authentication logic
        private readonly IAuthService _authService;

        // Constructor of the controller
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        // Register
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            _authService.Register(request);
            return Ok("User registered successfully");
        }

        // Login -> JWT TOKEN
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var response = _authService.Login(request);

            if (response == null)
                return Unauthorized("Invalid credentials");

            return Ok(response);
        }

        // Secure API
        [Authorize]
        [HttpGet("secure")]
        public IActionResult Secure()
        {
            return Ok("You accessed a protected API");
        }


        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshTokenRequest request)
        {
            var newAccessToken = _authService.RefreshAccessToken(request.RefreshToken);

            if (newAccessToken == null)
                return Unauthorized("Invalid refresh token");

            return Ok(new { AccessToken = newAccessToken });
        }



    }
}
        
using AuthAPI.Models;
using AuthAPI.Services;
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

        // Defines an HTTP POST endpoint.
        [HttpPost("signup")]
        public IActionResult Signup(RegisterRequest request)
        {
            _authService.Register(request);
            return Ok("User registered successfully");
        }
        // Defines another HTTP POST endpoint
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            if (!_authService.Login(request))
                return Unauthorized("Invalid credentials");

            return Ok("Login successful");
        }
    }
}

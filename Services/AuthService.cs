using AuthAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Xml;
// Defines the service class that implements authentication logic

namespace AuthAPI.Services
{
    // Implements IAuthService interface(dependency injection)
    public class AuthService : IAuthService
    {
        // stores registerd users
        // static=shared across requests(demo purpose)
        private static List<User> users = new();
        // stores refresh tokens mapped to users
        //used for token renewal
        private static List<RefreshToken> refreshTokens = new();

        // Configuration Injection
       // Reads values from appsettings.json
        private readonly IConfiguration _config;

        // Constructor injection
        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        // Register
        public void Register(RegisterRequest request)
        {
            users.Add(new User
            {
                Id = users.Count + 1,
                Username = request.Username,
                Password = request.Password,
                Role = "User",
                FullName = request.FullName,
                Email = request.Email
            });
        }

        // Login
        public AuthResponse Login(LoginRequest request)
        {
            // Authenticate user and returns token
            var user = users.SingleOrDefault(u =>
                u.Username == request.Username &&
                u.Password == request.Password);
            
            // find user matching credentials
            if (user == null)
                return null;

            // Token Generation
            // Creates a JWT access token
            var accessToken = GenerateJwtToken(user);
            // Creates a refresh token
            var refreshToken = GenerateRefreshToken();

            // Saves refresh token 
            refreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                Username = user.Username,
                ExpiryDate = DateTime.Now.AddDays(7)
            });
            // Return Tokens
            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        // Refresh Access Token
        public string RefreshAccessToken(string refreshToken)
        {
            // Generates new access token using refresh token
            var storedToken = refreshTokens.SingleOrDefault(rt =>
                rt.Token == refreshToken &&
                rt.ExpiryDate > DateTime.Now);
            // validates refresh token & expiry
            if (storedToken == null)
                return null;

            var user = users.Single(u => u.Username == storedToken.Username);
            return GenerateJwtToken(user);
        }

        // Generate Refresh Token
        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

        // Generate JWT Token
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };
            // Reads secret key from appsettings.json
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );
            // Signing Credentials
            var creds = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256
            );
            // JWT Creation
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

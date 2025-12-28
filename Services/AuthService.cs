using AuthAPI.Models;

namespace AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        // Declares a static list of user objects
        // static means shared across all instances of AuthService
        private static List<User> users = new();

        // Public method to register a new user
        public void Register(RegisterRequest request)
        {
            // Adds a new user object to the users list
            users.Add(new User
            {
                Id = users.Count + 1,
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = request.Password
            });
        }

        // public method to authenticate a user.
        public bool Login(LoginRequest request)
        {
            return users.Any(u =>
                u.Email == request.Email &&
                u.PasswordHash == request.Password);
        }
    }
}

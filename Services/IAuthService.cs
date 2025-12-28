using AuthAPI.Models;

namespace AuthAPI.Services
{
    // Declares a public interface named IAuthService
    // An interface defines what methods must be implemented 
    public interface IAuthService
    {
        // Declares a method named Register.
        // RegisterRequest request takes user registration data[FullName, Email, Password]
        void Register(RegisterRequest request);
        
        // Declares a method named Login.
        // LoginRequest request takes login credentials(Email,Password)
        bool Login(LoginRequest request);
    }
}

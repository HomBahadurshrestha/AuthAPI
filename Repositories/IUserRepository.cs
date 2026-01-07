using AuthAPI.Models;
// Repositories groups all repository-related interfaces and classes
// keeps the project organized and maintainable
namespace AuthAPI.Repositories
{
    // interface-> A Contract
    // IUserRepository: Defines what methods must be implemented
    public interface IUserRepository
    {
        //Represents a collection of users
        // Allows iteration using foreach
        IEnumerable<UserProfile> GetAll();
        UserProfile GetById(int id);
        void Add(UserProfile user);

    }
}

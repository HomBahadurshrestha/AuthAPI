using AuthAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Repositories
{
    //implements the interface(:IUserRepository)
    public class UserRepository : IUserRepository
    {
        // static-> shared across all instances of UserRepository
        //List<UserProfile> stores user data
        private static readonly List<UserProfile> users = new()
        {
            new UserProfile{Id=1, Username="admin", Email="admin@test.com"}
        };
        // public method required by IUserRepository 
        public IEnumerable<UserProfile> GetAll()
        {
            return users;
        }
        public UserProfile GetById(int id)
        {
            return users.FirstOrDefault(u => u.Id == id);
        }
        public void Add(UserProfile user)
        {
            users.Add(user);
        }
    }
}

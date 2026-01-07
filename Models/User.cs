namespace AuthAPI.Models
{
    public class User
    {
        public int Id { get; set; }   // <-- Add this line
        public string Username { get; set; }    
        public string Password { get; set; }
        public string Role { get; set; }

        public  string FullName { get; set; }
        public  string Email { get; set; }
        public  string PasswordHash { get; set; }
    }
}

namespace CliniCareApp.Models
{
    public class User
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? AccessKey { get; set; } 
    
        public User(string? userName, string? password, string? email, string? accesskey)
        {
            UserName = userName;
            Password = password;
            Email = email;
            AccessKey = accesskey;
        }
    }
}

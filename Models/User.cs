namespace ClinicApp.Models
{
    public class User
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? AccessKey { get; set; } 

        private  static List<User> Users = new  List<User>();

        public static User? GetUserByUserName(string userUserName)
        {
            return Users.FirstOrDefault(user => user.UserName == userUserName);
        }
    
        public User(string? userName, string? password, string? email, string? accesskey)
        {
            UserName = userName;
            Password = password;
            Email = email;
            AccessKey = accesskey;
        }
    }
}

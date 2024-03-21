namespace CliniCareApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? AccessKey { get; set; } 

        private static int NextUserId = 1;
    
        public User(string? userName, string? password, string? email, string? accesskey)
        { 
            Id = NextUserId;
            UserName = userName;
            Password = password;
            Email = email;
            AccessKey = accesskey;
        }

         public static void UpdateNextUserId(int nextId)
        {
            NextUserId = nextId;
        }
    }
}

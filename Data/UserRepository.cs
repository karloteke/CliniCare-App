using System.Text.Json;
using CliniCareApp.Models;

namespace CliniCareApp.Data
{
    public class UserRepository : IUserRepository
    {
        private List<User> _users = new List<User>();
        private readonly string _filePath;
        public UserRepository()
        {
            _filePath = GetFilePath();
            LoadUsers();
        }

        private string GetFilePath()
        {
            string basePath = Environment.GetEnvironmentVariable("IS_DOCKER") == "true" ? "SharedFolder" : "";
            return Path.Combine(basePath, "users.json");
        }

        public void AddUser(User user)
        {
            // Verifica si ya existe el usuario en la lista antes de agregarlo
            if (!_users.Any(u => u.UserName == user.UserName))
            {
                _users.Add(user);
                SaveChanges();
            }
        }

        public User? GetUserByUserName(string userName)
        {
            return _users.FirstOrDefault(u => u.UserName == userName);
        }
        public void UpdateUser(User user)
        {
            AddUser(user);
        }

        public void SaveChanges()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_users, options);
            File.WriteAllText(_filePath, jsonString);
        }

        private void LoadUsers()
        {
            if (File.Exists(_filePath))
            {
                string jsonString = File.ReadAllText(_filePath);
                var users = JsonSerializer.Deserialize<List<User>>(jsonString);
                _users = users ?? new List<User>();
            }
        }
    }
}

using RabbitMQ.Client;
using MySql.Data.MySqlClient;
using Dapper;
using Task_Novin_Teck.Model;
using Task_Novin_Teck.DTO;
using Task_Novin_Teck.RabbitMQ;
using Microsoft.Extensions.Caching.Memory;

namespace Task_Novin_Teck.Repository
{
    public class UserRepository
    {
        private readonly string _connectionString;
        private readonly IMemoryCache _cache;
        private readonly ConnectionFactory _rabbitMqFactory;

        public UserRepository(string connectionString, IMemoryCache cache)
        {
            _connectionString = connectionString;
            _cache = cache;
            _rabbitMqFactory = new ConnectionFactory() { HostName = RabbitMqInfo.HostName, UserName = RabbitMqInfo.UserName, Password = RabbitMqInfo.Password };
        }

        public void InsertUser(UserDto user)
        {
            // Publish a message to RabbitMQ queue
            Rabbitmq.PublishMessageToQueue($"New user created: Name={user.Name}, Email={user.Email}, Age={user.Age}", _rabbitMqFactory);

            // Insert user into database
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Users (Name, Email, Age) VALUES (@Name, @Email, @Age)";
                connection.Execute(query, new { Name = user.Name, Email = user.Email, Age = user.Age });
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users";
                return connection.Query<User>(query);
            }
        }

        public User GetUserById(int userId)
        {
            // check cache
            if (_cache.TryGetValue($"User_{userId}", out User cachedUser))
            {
                return cachedUser;
            }

            // if cache does not have this user
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE Id = @UserId";
                var user = connection.QueryFirstOrDefault<User>(query, new { UserId = userId });

                _cache.Set($"User_{userId}", user, TimeSpan.FromMinutes(10));

                return user;
            }
        }

        
    }
}

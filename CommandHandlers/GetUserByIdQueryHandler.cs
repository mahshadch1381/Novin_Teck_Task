using Dapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using MySql.Data.MySqlClient;
using Task_Novin_Teck.ReqClasses;
using Task_Novin_Teck.Model;

namespace Task_Novin_Teck.CommandHandlers
{
    public class GetUserByIdQueryHandler
    {
        private readonly string _connectionString;
        private readonly IMemoryCache _cache;

        public GetUserByIdQueryHandler(string connectionString, IMemoryCache cache)
        {
            _connectionString = connectionString;
            _cache = cache;
        }

        public User Handle(GetUserByIdQuery id)
        {
            // check cache
            if (_cache.TryGetValue($"User_{id.UserId}", out User cachedUser))
            {
                return cachedUser;
            }

            // if cache does not have this user
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE Id = @UserId";
                var user = connection.QueryFirstOrDefault<User>(query, new { UserId = id.UserId });

                _cache.Set($"User_{id.UserId}", user, TimeSpan.FromMinutes(10));

                return user;
            }
        }
    }
}

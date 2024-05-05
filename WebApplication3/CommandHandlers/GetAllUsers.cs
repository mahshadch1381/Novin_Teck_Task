using MySql.Data.MySqlClient;
using Task_Novin_Teck.ReqClasses;
using Task_Novin_Teck.Model;
using Dapper;

namespace Task_Novin_Teck.CommandHandlers
{
    public class GetAllUsers
    {

        private readonly string _connectionString;
        public GetAllUsers(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<User> Handle()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users";
                return connection.Query<User>(query);
            }
        }
    }
}

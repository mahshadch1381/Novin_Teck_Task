using Dapper;
using MediatR;
using MySql.Data.MySqlClient;
using Task_Novin_Teck.Model;
using Task_Novin_Teck.ReqClasses;


namespace Task_Novin_Teck.CommandHandlers
{
    public class CreateUserCommandHandler

    {
        private readonly string _connectionString;

        public CreateUserCommandHandler(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Handle(CreateUserCommand command)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Users (Name, Email, Age) VALUES (@Name, @Email, @Age)";
                connection.Execute(query, new { command.Name, command.Email, command.Age });
            }
        }
    }
}

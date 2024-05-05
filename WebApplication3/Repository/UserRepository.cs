using RabbitMQ.Client;
using MySql.Data.MySqlClient;
using Dapper;
using Task_Novin_Teck.Model;
using Task_Novin_Teck.DTO;
using Task_Novin_Teck.RabbitMQ;
using Microsoft.Extensions.Caching.Memory;
using Task_Novin_Teck.ReqClasses;
using MediatR;
using Task_Novin_Teck.CommandHandlers;

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

        public void InsertUser(CreateUserCommand command)
        {
            // Publish a message to RabbitMQ queue
            Rabbitmq.PublishMessageToQueue($"New user created: Name={command.Name}, Email={command.Email}, Age={command.Age}", _rabbitMqFactory);

            var handler = new CreateUserCommandHandler(_connectionString);
            handler.Handle(command);


        }

        public IEnumerable<User> GetAllUsers()
        {
            var handler = new GetAllUsers(_connectionString);
            return handler.Handle();
            
        }

        public  User GetUserById(GetUserByIdQuery query)
        {
            // use cache too.
            var handler = new GetUserByIdQueryHandler(_connectionString,_cache);
            return handler.Handle(query);
        }



    }
}

using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Task_Novin_Teck.RabbitMQ
{
    public class UserCreationConsumer : BackgroundService
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public UserCreationConsumer()
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _channel.QueueDeclare(queue: "user-queue",
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received message: {message}");

                // Perform additional task service like sending email to them that welcome

                Console.WriteLine("Processing complete.");
            };
            _channel.BasicConsume(queue: "user-queue",
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine("User creation consumer started. Waiting for messages...");
            await Task.Delay(Timeout.Infinite, stoppingToken); // Keep the consumer running until the application is stopped
        }

        public override void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
            base.Dispose();
        }
    }
}

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Task_Novin_Teck.RabbitMQ
{
    public class Rabbitmq
    {
       
        public static void PublishMessageToQueue(string message, ConnectionFactory _rabbitMqFactory)
        {
            using (var connection = _rabbitMqFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "user-queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "", routingKey: "user-queue", basicProperties: null, body: body);
            }
        }

        public static void ConsumeMessagesFromQueue(ConnectionFactory _rabbitMqFactory)
        {
            using (var connection = _rabbitMqFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "user-queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("Received message: {0}", message);
                };
                channel.BasicConsume(queue: "user-queue", autoAck: true, consumer: consumer);
            }
        }
    }
}

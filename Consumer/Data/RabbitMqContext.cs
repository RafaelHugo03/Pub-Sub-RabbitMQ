using RabbitMQ.Client;

namespace Consumer.Data
{
    public class RabbitMqContext
    {
        public static IModel GetRabbitMqConnection()
        {
            var connectionFactory = new ConnectionFactory {
                HostName = "localhost",
                UserName = "admin",
                Password = "123456"
            };

            var _connection = connectionFactory.CreateConnection("Rabbit-Learning");

            return _connection.CreateModel();
        }
    }
}
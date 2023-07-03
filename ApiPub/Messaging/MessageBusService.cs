using System.Text;
using ApiPub.Data;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace ApiPub.Messaging
{
    public class MessageBusService : IMessageBusService
    {        
        private readonly IModel channel;
        private const string exchange = "Product_Exchange";
        private const string productQueue = "Product_Queue";

        public MessageBusService()
        {
            channel = RabbitMqContext.GetRabbitMqConnection();
        }
        public void Publish(object data, string routingKey)
        {
            var type = data.GetType();

            var payload = JsonConvert.SerializeObject(data);
            var byteArray = Encoding.UTF8.GetBytes(payload);

            channel.ExchangeDeclare(exchange, ExchangeType.Direct, false, false, null);
            channel.QueueDeclare(productQueue, false, false, false, null);
            channel.QueueBind(productQueue, exchange, routingKey, null);
            channel.BasicPublish(exchange, routingKey, null, byteArray);

            Console.WriteLine($"{type.Name} Published");
        }
    }
}
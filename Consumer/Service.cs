using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Consumer.Entity;
using Consumer.Data;

namespace Consumer
{
    public class Service
    {
        private readonly IModel channel;
        private readonly string productQueue = "Product_Queue";

        public Service()
        {
            channel = RabbitMqContext.GetRabbitMqConnection();
        }
        public void Consume()
        {
            channel.QueueDeclare(productQueue, false, false, false, null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                var message = System.Text.Json.JsonSerializer.Deserialize<Product>(json);

                Console.WriteLine($"Titulo: {message.Id}; Texto={message.Name}");
            };
            channel.BasicConsume(productQueue, true, consumer);
        }
    }
}
using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace ProductInventoryChecker.Application
{
    public class MessageService
    {
        private readonly string _bootstrapServers = "localhost:9092"; // endereço do Kafka
        private readonly string _topic = "Novos-Produtos";

        public async Task SendMessageAsync(string message)
        {
            var config = new ProducerConfig { BootstrapServers = _bootstrapServers };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var deliveryResult = await producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
                    Console.WriteLine($"Mensagem enviada para o Kafka: {deliveryResult.Value}");
                }
                catch (ProduceException<Null, string> ex)
                {
                    Console.WriteLine($"Falha ao enviar mensagem: {ex.Message}");
                }
            }
        }
    }
}

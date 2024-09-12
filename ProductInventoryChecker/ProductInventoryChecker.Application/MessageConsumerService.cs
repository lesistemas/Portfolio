using Confluent.Kafka;
using System;
using System.Threading;

namespace ProductInventoryChecker.Application
{
    public class MessageConsumerService
    {
        private readonly string _bootstrapServers = "localhost:9092"; // endereço do Kafka
        private readonly string _groupId = "product-group";
        private readonly string _topic = "Novos-Produtos";

        public void ConsumeMessages(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = _groupId,
                BootstrapServers = _bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
            {
                consumer.Subscribe(_topic);

                try
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        var consumeResult = consumer.Consume(cancellationToken);
                        Console.WriteLine($"Mensagem recebida: {consumeResult.Message.Value}");
                        // Aqui você pode processar a mensagem, por exemplo, salvar no banco ou disparar um evento.
                    }
                }
                catch (ConsumeException ex)
                {
                    Console.WriteLine($"Erro ao consumir mensagem: {ex.Message}");
                }
            }
        }
    }
}

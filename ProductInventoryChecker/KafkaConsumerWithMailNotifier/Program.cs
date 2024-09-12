using Confluent.Kafka;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    private static async Task Main(string[] args)
    {
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092", // Alterar para o seu servidor Kafka
            GroupId = "email-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };

        var topic = "Novos-Produtos";

        using (var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build())
        {
            consumer.Subscribe(topic);

            Console.WriteLine("Consumindo mensagens do tópico Kafka...");

            while (true)
            {
                try
                {
                    var cr = consumer.Consume(CancellationToken.None);
                    var message = cr.Value;
                    Console.WriteLine($"Received message: {message}");

                    await SendEmailAsync("recipient@example.com", "New Kafka Message", message);

                    consumer.Commit(cr); // Commit the offset after processing the message
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error occurred: {e.Error.Reason}");
                }
            }
        }



    }

    private static async Task SendEmailAsync(string to, string subject, string body)
    {
        var smtpClient = new SmtpClient("smtp.your-email-provider.com")
        {
            Port = 587, // Altere para a porta do seu servidor SMTP
            Credentials = new NetworkCredential("your-email@example.com", "your-email-password"),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress("your-email@example.com"),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(to);

        try
        {
            await smtpClient.SendMailAsync(mailMessage);
            Console.WriteLine("Email sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
        }
    }
}

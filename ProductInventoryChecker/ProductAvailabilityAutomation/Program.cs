using System;
using System.Timers;
using System.Threading.Tasks;
using ProductInventoryChecker.Application;

namespace ProductAvailabilityAutomation
{
    class Program
    {
        private static System.Timers.Timer timer;
        private static ProductAvailabilityRunner runner;

        static void Main()
        {
            runner = new ProductAvailabilityRunner();

            // Configura o timer para 1 minuto (1 * 60 * 1000 ms)
            timer = new System.Timers.Timer(1 * 60 * 1000);
            timer.Elapsed += OnTimer;
            timer.AutoReset = true;
            timer.Enabled = true;

            Console.WriteLine("Pressione Ctrl+C para sair.");
            // Mantém o console rodando
            Task.Delay(-1).Wait();
        }

        private static async void OnTimer(object sender, ElapsedEventArgs e)
        {
            await runner.ExecuteTask();
        }
    }
}
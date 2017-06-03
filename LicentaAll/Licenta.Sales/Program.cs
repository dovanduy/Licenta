using System;
using GreenPipes;
using Licenta.Messaging;
using Licenta.Sales.Consumers;
using MassTransit;

namespace Licenta.Sales
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Sales";

            var bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, RabbitMqConstants.SalesServiceQueue,
                    e =>
                    {
                        e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                        e.Consumer<ProductUpdatedEventConsumer>();
                    });
            });

            bus.Start();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sales service started listening... Press [ENTER] to exit");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.ReadLine();

            bus.Stop();
        }
    }
}

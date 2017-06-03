using System;
using GreenPipes;
using Licenta.Inventory.Consumers;
using Licenta.Messaging;
using MassTransit;

namespace Licenta.Inventory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Inventory";

            var bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, RabbitMqConstants.InventoryServiceQueue,
                    e =>
                    {
                        e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                        e.Consumer<ProductUpdatedEventConsumer>();
                    });
            });

            bus.Start();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Inventory service started listening... Press [ENTER] to exit");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.ReadLine();

            bus.Stop();
        }
    }
}

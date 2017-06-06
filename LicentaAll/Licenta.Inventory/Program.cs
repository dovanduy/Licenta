using System;
using GreenPipes;
using Licenta.Inventory.Consumers;
using Licenta.Messaging;
using Licenta.Messaging.Generic;
using MassTransit;
using Microsoft.Practices.Unity;

namespace Licenta.Inventory
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Inventory";

            IUnityContainer container = CreateContainer();

            var bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, RabbitMqConstants.InventoryServiceQueue,
                    e =>
                    {
                        e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                        e.Consumer<ProductUpdatedEventConsumer>(container);
                    });
            });

            bus.ConnectConsumeObserver(new ConsumeObserver());
            bus.ConnectPublishObserver(new PublishObserver());

            bus.Start();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Inventory service started listening... Press [ENTER] to exit");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.ReadLine();

            bus.Stop();
        }
    }
}

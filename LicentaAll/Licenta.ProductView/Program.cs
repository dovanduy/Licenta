using GreenPipes;
using Licenta.Messaging;
using Licenta.ProductView.Consumers;
using MassTransit;
using System;
using System.Collections.Generic;
using Licenta.Messaging.Generic;

namespace Licenta.ProductView
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "ProductView";

            var bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductViewQueue + ".product.updated",
                    e =>
                    {
                        e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                        e.Consumer<ProductUpdatedEventConsumer>();
                    });

                cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductViewQueue + ".product.inventoryupdated",
                    e =>
                    {
                        e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                        e.Consumer<ProductInventoryUpdatedEventConsumer>();
                    });

                cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductViewQueue + ".product.priceupdated",
                    e =>
                    {
                        e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                        e.Consumer<ProductPriceUpdatedEventConsumer>();
                    });

                cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductViewQueue + ".product.ratingupdated",
                    e =>
                    {
                        e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                        e.Consumer<ProductRatingUpdatedEventConsumer>();
                    });

                cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductViewQueue + ".product.deleted",
                    e =>
                    {
                        e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                        e.Consumer<ProductDeletedEventConsumer>();
                    });

                cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductViewQueue + ".category.deleted",
                    e =>
                    {
                        e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                        e.Consumer<CategoryDeletedEventConsumer>();
                    });

                cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductViewQueue + ".category.updated",
                    e =>
                    {
                        e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                        e.Consumer<CategoryUpdatedEventConsumer>();
                    });
            });

            bus.ConnectConsumeObserver(new ConsumeObserver());

            bus.Start();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ProductView service started listening... Press [ENTER] to exit");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.ReadLine();

            bus.Stop();
        }
    }
}

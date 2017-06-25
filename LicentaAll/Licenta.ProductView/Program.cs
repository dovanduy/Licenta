using GreenPipes;
using Licenta.Messaging;
using Licenta.ProductView.Consumers;
using MassTransit;
using System;
using Licenta.Messaging.Generic;
using MassTransit.RabbitMqTransport;
using Microsoft.Practices.Unity;

namespace Licenta.ProductView
{
    partial class Program
    {
        private static IUnityContainer container;

        static void Main(string[] args)
        {
            Console.Title = "ProductView";

            var bus = BusConfigurator.ConfigureBus(ConfigureBus);

            bus.ConnectConsumeObserver(new ConsumeObserver());

            bus.Start();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ProductView service started listening... Press [ENTER] to exit");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.ReadLine();

            bus.Stop();
        }

        private static void ConfigureBus(IRabbitMqBusFactoryConfigurator cfg, IRabbitMqHost host)
        {
            container = CreateContainer();

            cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductViewQueue + ".product.updated",
                ConfigureEndpoint<ProductUpdatedEventConsumer>);

            cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductViewQueue + ".product.inventoryupdated",
                ConfigureEndpoint<ProductInventoryUpdatedEventConsumer>);

            cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductViewQueue + ".product.priceupdated",
                ConfigureEndpoint<ProductPriceUpdatedEventConsumer>);

            cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductViewQueue + ".product.ratingupdated",
                ConfigureEndpoint<ProductRatingUpdatedEventConsumer>);

            cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductViewQueue + ".product.deleted",
                ConfigureEndpoint<ProductDeletedEventConsumer>);

            cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductViewQueue + ".category.deleted",
                ConfigureEndpoint<CategoryDeletedEventConsumer>);

            cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductViewQueue + ".category.updated",
                ConfigureEndpoint<CategoryUpdatedEventConsumer>);
        }

        private static void ConfigureEndpoint<T>(IRabbitMqReceiveEndpointConfigurator obj) where T : class,IConsumer
        {
            obj.UseRetry(retryCfg => { retryCfg.Immediate(5); });
            obj.Consumer<T>(container);
        }
    }
}

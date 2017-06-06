using GreenPipes;
using Licenta.Messaging;
using Licenta.Products.Consumers;
using MassTransit;
using System;
using Licenta.Messaging.Generic;

namespace Licenta.Products
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Products";
            var container = CreateContainer();
            var bus =
                BusConfigurator.ConfigureBus((cfg, host) =>
                {
                    cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductServiceQueue + ".product.add",
                        e =>
                        {
                            e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                            e.Consumer<AddProductCommandConsumer>(container);
                        });
                    cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductServiceQueue + ".product.update",
                        e =>
                        {
                            e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                            e.Consumer<UpdateProductCommandConsumer>(container);
                        });
                    cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductServiceQueue + ".product.delete",
                        e =>
                        {
                            e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                            e.Consumer<DeleteProductCommandConsumer>(container);
                        });
                    cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductServiceQueue + ".category.add",
                        e =>
                        {
                            e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                            e.Consumer<AddCategoryCommandConsumer>(container);
                        });
                    cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductServiceQueue + ".category.update",
                        e =>
                        {
                            e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                            e.Consumer<UpdateCategoryCommandConsumer>(container);
                        });
                    cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductServiceQueue + ".category.delete",
                        e =>
                        {
                            e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                            e.Consumer<DeleteCategoryCommandConsumer>(container);
                        });
                });

            bus.ConnectConsumeObserver(new ConsumeObserver());
            bus.ConnectPublishObserver(new PublishObserver());

            bus.Start();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Product service started listening... Press [ENTER] to exit");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.ReadLine();

            bus.Stop();
        }
    }
}

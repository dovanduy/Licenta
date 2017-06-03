using GreenPipes;
using Licenta.Messaging;
using Licenta.Products.Consumers;
using MassTransit;
using System;
using System.Collections.Generic;

namespace Licenta.Products
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Products";

            var bus =
                BusConfigurator.ConfigureBus((cfg, host) =>
                {
                    cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductServiceQueue + ".product.add",
                        e =>
                        {
                            e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                            e.Consumer<AddProductCommandConsumer>();
                        });
                    cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductServiceQueue + ".product.update",
                        e =>
                        {
                            e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                            e.Consumer<UpdateProductCommandConsumer>();
                        });
                    cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductServiceQueue + ".product.delete",
                        e =>
                        {
                            e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                            e.Consumer<DeleteProductCommandConsumer>();
                        });
                    cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductServiceQueue + ".category.add",
                        e =>
                        {
                            e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                            e.Consumer<AddCategoryCommandConsumer>();
                        });
                    cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductServiceQueue + ".category.update",
                        e =>
                        {
                            e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                            e.Consumer<UpdateCategoryCommandConsumer>();
                        });
                    cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductServiceQueue + ".category.delete",
                        e =>
                        {
                            e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                            e.Consumer<DeleteCategoryCommandConsumer>();
                        });
                });
            
           
            bus.Start();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Product service started listening... Press [ENTER] to exit");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.ReadLine();

            bus.Stop();
        }
    }
}

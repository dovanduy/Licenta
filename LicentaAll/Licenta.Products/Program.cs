using Licenta.Messaging;
using Licenta.Products.Consumers;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Products
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Products";

            var bus = BusConfigurator.ConfigureBus((cfg,host) => {
                cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductServiceQueue, 
                    e => {
                        e.Consumer<AddProductCommandConsumer>();
                 });
            });

            bus.Start();

            Console.WriteLine("Product service started listening... Press [ENTER] to exit");

            Console.ReadLine();

            bus.Stop();
        }
    }
}

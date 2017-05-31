using GreenPipes;
using Licenta.Messaging;
using Licenta.ProductView.Consumers;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.ProductView
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "ProductView";

            var bus = BusConfigurator.ConfigureBus((cfg, host) => {
                cfg.ReceiveEndpoint(host, RabbitMqConstants.ProductServiceQueue,
                    e => {
                        e.UseRetry(retryCfg => { retryCfg.Immediate(20); });
                        e.Consumer<ProductAddedEventConsumer>();
                    });
            });

            bus.Start();

            Console.WriteLine("ProductView service started listening... Press [ENTER] to exit");

            Console.ReadLine();

            bus.Stop();
        }
    }
}

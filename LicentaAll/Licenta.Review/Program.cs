using GreenPipes;
using Licenta.Messaging;
using Licenta.Review.Consumers;
using MassTransit;
using System;
using System.Collections.Generic;
using Licenta.Messaging.Generic;

namespace Licenta.Review
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Review";

            var container = CreateContainer();

            var bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, RabbitMqConstants.ReviewServiceQueue + ".review.add",
                    e =>
                    {
                        e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                        e.Consumer<AddReviewCommandConsumer>(container);
                    });

                cfg.ReceiveEndpoint(host, RabbitMqConstants.ReviewServiceQueue + ".review.update",
                    e =>
                    {
                        e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                        e.Consumer<UpdateReviewCommandConsumer>(container);
                    });

                cfg.ReceiveEndpoint(host, RabbitMqConstants.ReviewServiceQueue + ".review.delete",
                    e =>
                    {
                        e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                        e.Consumer<DeleteReviewCommandConsumer>(container);
                    });

                cfg.ReceiveEndpoint(host, RabbitMqConstants.ReviewServiceQueue + ".product.deleted",
                    e =>
                    {
                        e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                        e.Consumer<ProductDeletedEventConsumer>(container);
                    });
            });

            bus.ConnectConsumeObserver(new ConsumeObserver());
            bus.ConnectPublishObserver(new PublishObserver());

            bus.Start();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Review service started listening... Press [ENTER] to exit");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.ReadLine();

            bus.Stop();
        }
    }
}

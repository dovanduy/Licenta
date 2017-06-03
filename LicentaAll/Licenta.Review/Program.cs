﻿using GreenPipes;
using Licenta.Messaging;
using Licenta.Review.Consumers;
using MassTransit;
using System;
using System.Collections.Generic;

namespace Licenta.Review
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Review";

            List<IBusControl> busses = new List<IBusControl>
            {
                BusConfigurator.ConfigureBus((cfg, host) =>
                {
                    cfg.ReceiveEndpoint(host, RabbitMqConstants.ReviewServiceQueue + ".review.add",
                        e =>
                        {
                            e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                            e.Consumer<AddReviewCommandConsumer>();
                        });
                
                    cfg.ReceiveEndpoint(host, RabbitMqConstants.ReviewServiceQueue + ".review.update",
                        e =>
                        {
                            e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                            e.Consumer<UpdateReviewCommandConsumer>();
                        });

                    cfg.ReceiveEndpoint(host, RabbitMqConstants.ReviewServiceQueue + ".review.delete",
                        e =>
                        {
                            e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                            e.Consumer<DeleteReviewCommandConsumer>();
                        });

                    cfg.ReceiveEndpoint(host, RabbitMqConstants.ReviewServiceQueue + ".product.deleted",
                        e =>
                        {
                            e.UseRetry(retryCfg => { retryCfg.Immediate(5); });
                            e.Consumer<ProductDeletedEventConsumer>();
                        });
                })
            };

            foreach(IBusControl bus in busses)
                bus.Start();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Review service started listening... Press [ENTER] to exit");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.ReadLine();

            foreach (IBusControl bus in busses)
                bus.Stop();
        }
    }
}
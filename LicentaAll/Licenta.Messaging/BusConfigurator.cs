using MassTransit;
using MassTransit.RabbitMqTransport;
using System;

namespace Licenta.Messaging
{
    public static class BusConfigurator
    {
        public static IBusControl ConfigureBus(Action<IRabbitMqBusFactoryConfigurator,IRabbitMqHost> registrationAction=null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(RabbitMqConstants.RabbitMqUri), hst =>
                {
                    hst.Username(RabbitMqConstants.Username);
                    hst.Password(RabbitMqConstants.Password);
                });
                registrationAction?.Invoke(cfg, host);
            });
        }
    }
}

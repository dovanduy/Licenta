using System;
using Licenta.Messaging;
using MassTransit;

namespace LicentaHighLevelApi.Services
{
    public class BusMessagingService : IDisposable
    {
        public IBusControl BusControl { get; }

        public BusMessagingService()
        {
            BusControl = BusConfigurator.ConfigureBus();
            BusControl.Start();
        }

        public void Dispose()
        {
            BusControl.Stop();
        }
    }
}
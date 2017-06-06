using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;

namespace Licenta.Messaging.Generic
{
    public class PublishObserver : IPublishObserver
    {
        public async Task PrePublish<T>(PublishContext<T> context)
            where T : class
        {
            string messageName = GetMessageName<T>();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            await Console.Out.WriteLineAsync($"Publishing {messageName}...");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public async Task PostPublish<T>(PublishContext<T> context)
            where T : class
        {
            string messageName = GetMessageName<T>();
            Console.ForegroundColor = ConsoleColor.Yellow;
            await Console.Out.WriteLineAsync($"Published {messageName} successfully");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public async Task PublishFault<T>(PublishContext<T> context, Exception exception)
            where T : class
        {
            string messageName = GetMessageName<T>();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            await Console.Out.WriteLineAsync($"Fault when publishing {messageName}");
            Console.ForegroundColor = ConsoleColor.Gray;
            await Console.Out.WriteLineAsync($"{exception}");
        }

        private string GetMessageName<T>() where T : class
        {
            return typeof(T).FullName.Split('.').Last();
        }
    }
}

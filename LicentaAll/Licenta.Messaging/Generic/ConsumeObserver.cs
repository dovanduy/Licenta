using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;

namespace Licenta.Messaging.Generic
{
    public class ConsumeObserver : IConsumeObserver
    {
        async Task IConsumeObserver.PreConsume<T>(ConsumeContext<T> context)
        {
            string messageName = GetMessageName<T>();
            Console.ForegroundColor = ConsoleColor.White;
            await Console.Out.WriteLineAsync($"Recieved {messageName}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        async Task IConsumeObserver.PostConsume<T>(ConsumeContext<T> context)
        {
            string messageName = GetMessageName<T>();
            Console.ForegroundColor = ConsoleColor.Green;
            await Console.Out.WriteLineAsync($"Successfully consumed {messageName}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        async Task IConsumeObserver.ConsumeFault<T>(ConsumeContext<T> context, Exception exception)
        {
            string messageName = GetMessageName<T>();
            Console.ForegroundColor = ConsoleColor.Red;
            await Console.Out.WriteLineAsync($"Fault when consuming {messageName}");
            Console.ForegroundColor = ConsoleColor.Gray;
            await Console.Out.WriteLineAsync($"Exception: {exception}");
        }

        private string GetMessageName<T>() where T : class
        {
            return typeof(T).FullName.Split('.').Last();
        }
    }
}



using System;
using System.Threading.Tasks;
using RabbitMQ.Client.Core.DependencyInjection;

namespace Monitoring.Queue.Event
{
    public class GeneralAsyncMessageHandler : IAsyncMessageHandler
    {
        public async Task Handle(string message, string routingKey)
        {
            await Task.Run(() => Console.Write(message));
        }
    }
}

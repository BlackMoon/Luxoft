using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using client.Handlers;
using Domain;

namespace client
{
    public class MessageDispatcher : IMessageDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDictionary<string, int> _stat = new Dictionary<string, int>();

        public MessageDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public void Dispatch<TParameter>(TParameter message) where TParameter : IMessage
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var handler = _serviceProvider.GetRequiredService<IMessageHandler<TParameter>>();
            handler.Execute(message);

            // подсчет статистики
            string name = message.GetType().Name;

            if (_stat.ContainsKey(name))
                _stat[name] = _stat[name] + 1;
            else
                _stat[name] = 1;
        }

        public void ShowStat()
        {
            Console.WriteLine("Обработано сообщений:\n");

            foreach (KeyValuePair<string, int> kvp in _stat)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");    
            }
        }
    }
}

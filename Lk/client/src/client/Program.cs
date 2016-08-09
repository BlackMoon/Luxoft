using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using client.DynamicXml;
using client.Handlers;
using Domain;
using DryIoc;
using Microsoft.Extensions.DependencyInjection;
using DryIoc.AspNetCore.DependencyInjection;

namespace client
{
    public class Program
    {
        private const int Port = 1000;
        private const string Server = "server";

        public static void Main(string[] args)
        {
            IContainer container = ConfigureServices();

            IMessageDispatcher dispatcher = container.Resolve<IMessageDispatcher>();

            using (TcpClient tcpClient = new TcpClient())
            {
                tcpClient.ConnectAsync(Server, Port);
                NetworkStream stream = tcpClient.GetStream();

                if (stream.CanRead)
                {
                    // Reads NetworkStream into a byte buffer.
                    byte[] bytes = new byte[tcpClient.ReceiveBufferSize];
                    stream.Read(bytes, 0, tcpClient.ReceiveBufferSize);

                    // получить xml документ
                    string returndata = Encoding.UTF8.GetString(bytes);
                    XDocument xdoc = XDocument.Load(returndata);

                    // в xml-документе элементы [messageType, header, body]
                    dynamic obj = xdoc.AsDynamic();

                    IMessage msg = container.Resolve<IMessage>(serviceKey: (string)obj.MessageType);
                    msg.Header = obj.Header;
                    msg.Body = obj.Body;

                    if (dispatcher != null)
                    {
                        dispatcher.Dispatch(msg);
                        dispatcher.ShowStat();
                    }
                }
                else
                {
                    Console.WriteLine("You cannot read data from this stream.");
                    stream.Dispose();
                }
            }
        }
        

        /// <summary>
        /// Регистрация служб в контейнере
        /// </summary>
        /// <returns></returns>
        private static IContainer ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IMessageDispatcher, MessageDispatcher>();

            // DryIoc container
            IContainer container = new Container().WithDependencyInjectionAdapter(services);

            // Get [domain] assembly
            AssemblyName an = Assembly.GetEntryAssembly()
                .GetReferencedAssemblies()
                .SingleOrDefault(a => a.Name == "domain");

            // register [MessageA, MessageB, MessageC] types
            IEnumerable<Type> types = Assembly.Load(an).GetTypes().Where(t => t.GetInterfaces().Contains(typeof (IMessage)));

            foreach (Type t in types)
            {
                MessageTypeAttribute attr = t.GetTypeInfo().GetCustomAttribute<MessageTypeAttribute>();
                if (attr != null)
                {
                    container.Register(typeof(IMessage), t, serviceKey: attr.MessageType);
                }
            }

            container.Register<IMessageDispatcher, MessageDispatcher>();
            container.Register<IMessageHandler<MessageA>, MessageAHandler>();
            container.Register<IMessageHandler<MessageB>, MessageBHandler>();
            container.Register<IMessageHandler<MessageC>, MessageCHandler>();

            return container;
        }
    }
}

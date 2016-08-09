using System;
using System.IO;
using Domain;

namespace client.Handlers
{
    public class MessageBHandler : IMessageHandler<MessageB>
    {
        public void Execute(MessageB message)
        {
            if (message != null)
                File.WriteAllText("path2", message.ToString());
        }
    }
}

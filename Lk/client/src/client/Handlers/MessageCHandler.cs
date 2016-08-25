using System.IO;
using Domain;

namespace client.Handlers
{
    public class MessageCHandler: IMessageHandler<MessageC>
    {
        public void Execute(MessageC message)
        {
            if (message != null)
                File.WriteAllText("path3", message.ToString());
        }
    }
}

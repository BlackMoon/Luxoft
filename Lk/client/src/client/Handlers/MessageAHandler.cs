using System.IO;
using Domain;

namespace client.Handlers
{
    public class MessageAHandler: IMessageHandler<MessageA>
    {
        public void Execute(MessageA message)
        {
            if (message != null)
                File.WriteAllText("path1", message.ToString());
        }
    }
}

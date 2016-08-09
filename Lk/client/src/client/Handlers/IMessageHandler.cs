
using Domain;

namespace client.Handlers
{
    /// <summary>
    /// Base interface for message handlers
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
    public interface IMessageHandler<in TParameter> where TParameter : IMessage
    {
        /// <summary>
        /// Executes a command handler
        /// </summary>
        /// <param name="command">The command to be used</param>
        void Execute(TParameter command);
    }
}

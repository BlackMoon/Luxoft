using Domain;

namespace client
{
    interface IMessageDispatcher
    {
        /// <summary>
        /// Dispatches a command to its handler
        /// </summary>
        /// <typeparam name="TParameter">Message Type</typeparam>
        /// <param name="message">The message to be passed to the handler</param>
        void Dispatch<TParameter>(TParameter message) where TParameter : IMessage;
       
        /// <summary>
        /// Show statistics
        /// </summary>
        void ShowStat();
    }
}

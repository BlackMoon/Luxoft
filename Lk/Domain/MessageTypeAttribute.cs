using System;

namespace Domain
{
    /// <summary>
    /// Аттрибут - тип TCP/IP сообщения
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MessageTypeAttribute : Attribute
    {
        public MessageTypeAttribute(string messageType)
        {
            MessageType = messageType;
        }

        public virtual string MessageType { get; }

    }
}

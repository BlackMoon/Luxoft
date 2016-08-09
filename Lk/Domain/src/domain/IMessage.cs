namespace Domain
{
    /// <summary>
    /// Интерфейс TCP/IP сообщения
    /// </summary>
    public interface IMessage
    {
        string Header { get; set; }
        string Body { get; set; }
    }
}

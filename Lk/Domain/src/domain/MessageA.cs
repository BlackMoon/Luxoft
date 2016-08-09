namespace Domain
{
    [MessageType("A")]
    public class MessageA: IMessage
    {
        public string Header { get; set; }
        public string Body { get; set; }
    }
}

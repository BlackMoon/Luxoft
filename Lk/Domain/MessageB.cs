namespace Domain
{
    [MessageType("B")]
    public class MessageB : IMessage
    {
        public string Header { get; set; }
        public string Body { get; set; }
    }
}

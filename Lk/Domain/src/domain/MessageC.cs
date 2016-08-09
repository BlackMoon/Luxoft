namespace Domain
{
    [MessageType("C")]
    public class MessageC : IMessage
    {
        public string Header { get; set; }
        public string Body { get; set; }
    }
}

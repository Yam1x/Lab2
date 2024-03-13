using System;

namespace MessagesServer.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Data { get; set; } = string.Empty;
    }
}

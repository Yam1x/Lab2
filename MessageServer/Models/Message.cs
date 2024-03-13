using System;

namespace MessageServer.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Data { get; set; } = string.Empty;
    }
}

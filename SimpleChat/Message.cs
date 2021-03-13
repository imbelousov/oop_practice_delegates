using System;

namespace SimpleChat
{
    public abstract class Message
    {
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class TextMessage : Message
    {
        public string Text { get; set; }
    }

    public class BinaryMessage : Message
    {
        public string Title { get; set; }
        public byte[] Data { get; set; }
    }
}

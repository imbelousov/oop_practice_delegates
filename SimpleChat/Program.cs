using System;

namespace SimpleChat
{
    public static class Program
    {
        private static readonly MessageEncoder encoder = new MessageEncoder();

        public static void Main(string[] args)
        {
            Console.Title = "Simple chat";
            
            Console.Write("Enter your name: ");
            var userName = Console.ReadLine();
            Console.Clear();
            Console.Title = $"Simple chat [{userName}]";

            var peer = new Peer();
            peer.DataReceived += OnDataReceived;
            peer.Start();

            while (true)
            {
                var input = Console.ReadLine();
                var message = new TextMessage
                {
                    CreatedAt = DateTime.Now,
                    Author = userName,
                    Text = input
                };
                var bytes = encoder.Encode(message);
                peer.SendToAll(bytes);
            }
        }

        private static void OnDataReceived(object sender, DataReceivedEventArgs eventArgs)
        {
            var bytes = eventArgs.Data;
            var message = encoder.Decode(bytes);
            Console.WriteLine($"[{message.CreatedAt} {message.Author}] >> {message.Text}");
        }
    }
}

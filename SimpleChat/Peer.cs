using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleChat
{
    public class Peer
    {
        private const int UdpPortRangeStart = 18000;
        private const int UdpPortRangeLength = 8;

        private UdpClient listener;
        private IReadOnlyCollection<int> otherPeersPorts;
        private int port;

        public event EventHandler<DataReceivedEventArgs> DataReceived;

        public void Start()
        {
            DiscoverOtherPeers();
            InitListener();
            BackgroundLoop(() =>
            {
                DiscoverOtherPeers();
                Thread.Sleep(1000);
            });
            BackgroundLoop(Accept);
        }

        public void SendToAll(byte[] bytes)
        {
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);
            writer.Write(bytes.Length);
            writer.Write(bytes);
            writer.Flush();
            var bytesToSend = stream.ToArray();

            foreach (var otherPeerPort in otherPeersPorts)
            {
                var ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), otherPeerPort);
                var client = new UdpClient();
                try
                {
                    client.Connect(ep);
                    client.Send(bytesToSend, bytesToSend.Length);
                }
                catch (SocketException)
                {
                }
                finally
                {
                    client.Dispose();
                }
            }
        }

        private void Accept()
        {
            var ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            try
            {
                var bytes = listener.Receive(ref ep);
                var stream = new MemoryStream(bytes);
                var reader = new BinaryReader(stream, Encoding.UTF8);
                var length = reader.ReadInt32();
                var data = reader.ReadBytes(length);
                DataReceived?.Invoke(this, new DataReceivedEventArgs {Data = data});
            }
            catch (SocketException)
            {
            }
        }

        private void InitListener()
        {
            port = Enumerable.Range(UdpPortRangeStart, UdpPortRangeLength).First(x => !otherPeersPorts.Contains(x));
            listener = new UdpClient(port);
        }

        private void DiscoverOtherPeers()
        {
            otherPeersPorts = IPGlobalProperties.GetIPGlobalProperties()
                .GetActiveUdpListeners()
                .Select(x => x.Port)
                .Where(x => x >= UdpPortRangeStart && x < UdpPortRangeStart + UdpPortRangeLength)
                .Where(x => x != port)
                .ToHashSet();
        }

        private void BackgroundLoop(Action action)
        {
            _ = Task.Run(() =>
            {
                while (true)
                    action();
            });
        }
    }

    public class DataReceivedEventArgs : EventArgs
    {
        public byte[] Data { get; set; }
    }
}

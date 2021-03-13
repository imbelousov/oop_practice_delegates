using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChat
{
    public class Peer
    {
        private const string PipeNamePrefix = @"simple_chat_";

        public event EventHandler<DataReceivedEventArgs> OnDataReceived;

        public void Start()
        {
            _ = Task.Run(() =>
            {
                while (true)
                {
                    using (var pipe = new NamedPipeServerStream(GetNamedPipeName(Process.GetCurrentProcess().Id), PipeDirection.InOut))
                    {
                        pipe.WaitForConnection();
                        pipe.WaitForPipeDrain();
                        using (var reader = new BinaryReader(pipe, Encoding.UTF8))
                        {
                            var length = reader.ReadInt32();
                            var data = reader.ReadBytes(length);
                            OnDataReceived?.Invoke(this, new DataReceivedEventArgs {Data = data});
                        }
                    }
                }
            });
        }

        public void SendToAll(byte[] bytes)
        {
            var processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Where(x => x.Id != Process.GetCurrentProcess().Id);
            foreach (var process in processes)
            {
                using (var pipe = new NamedPipeClientStream(GetNamedPipeName(process.Id)))
                {
                    pipe.Connect(1000);
                    using (var writer = new BinaryWriter(pipe))
                    {
                        writer.Write(bytes.Length);
                        writer.Write(bytes);
                    }
                }
            }
        }

        private string GetNamedPipeName(int processId) => PipeNamePrefix + processId;
    }

    public class DataReceivedEventArgs : EventArgs
    {
        public byte[] Data { get; set; }
    }
}

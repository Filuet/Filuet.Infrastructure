using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Filuet.Infrastructure.Communication
{
    public class TcpChannel : ICommunicationChannel
    {
        public TcpChannel(string ip, ushort port = 5000, uint responseDelayMs = 100)
        {
            _ip = ip;
            _port = port;
            _responseDelayMs = responseDelayMs;
            _mutex = new object();
        }

        public byte[] SendCommand(byte[] data)
        {
            lock (_mutex)
            {
                TcpClient client = new TcpClient();
                client.Connect(new IPEndPoint(IPAddress.Parse(_ip), (int)_port));

                NetworkStream stream = client.GetStream();

                stream.Write(data, 0, data.Length);
                data = new byte[32];
                Thread.Sleep((int)_responseDelayMs);
                int bytes = stream.Read(data, 0, data.Length);

                stream.Close();
                client.Close();
                return data.Take(bytes).ToArray();
            }
        }

        private readonly string _ip;
        private readonly ushort _port;
        private readonly uint _responseDelayMs;
        private object _mutex;
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Filuet.Infrastructure.Communication
{
    public class TcpChannel : ICommunicationChannel
    {
        public TcpChannel(string ip, ushort port = 5000, byte? endOfPackage = null)
        {
            _ip = ip;
            _port = port;
            _endOfPackage = endOfPackage;
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
                List<byte> response = new List<byte>();

                if (_endOfPackage != null)
                {
                    while (true)
                    {
                        if (stream.DataAvailable)
                        {
                            byte nextByte = (byte)stream.ReadByte();
                            response.Add(nextByte);

                            if (nextByte == _endOfPackage)
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    while (stream.DataAvailable)
                    {
                        response.Add((byte)stream.ReadByte());
                    }
                }

                stream.Close();
                client.Close();
                return response.ToArray();
            }
        }

        private readonly string _ip;
        private readonly ushort _port;
        private readonly byte? _endOfPackage;
        private object _mutex;
    }
}
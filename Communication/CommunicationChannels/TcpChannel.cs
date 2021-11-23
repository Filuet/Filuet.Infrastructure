using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace Filuet.Infrastructure.Communication
{
    public class TcpChannel : ICommunicationChannel
    {
        public TcpChannel(string ip, ushort port = 5000, int awaitDelayMs = 300)
        {
            _ip = ip;
            _port = port;
            _mutex = new object();
            _awaitResponseDelayMs = awaitDelayMs;
        }

        public byte[] SendCommand(byte[] data, byte? endOfResponse = null)
        {
            lock (_mutex)
            {
                TcpClient client = new TcpClient();
                client.Connect(new IPEndPoint(IPAddress.Parse(_ip), (int)_port));

                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                List<byte> response = new List<byte>();

                if (endOfResponse != null)
                {
                    while (true)
                    {
                        if (stream.DataAvailable)
                        {
                            byte nextByte = (byte)stream.ReadByte();
                            response.Add(nextByte);

                            if (nextByte == endOfResponse)
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    while (true)
                    {
                        if (stream.DataAvailable)
                        {
                            sw.Reset();
                            sw.Start();
                            response.Add((byte)stream.ReadByte());
                        }

                        if (sw.ElapsedMilliseconds > _awaitResponseDelayMs)
                            break;
                    }
                }

                stream.Close();
                client.Close();
                return response.ToArray();
            }
        }

        private readonly string _ip;
        private readonly ushort _port;
        private object _mutex;
        private int _awaitResponseDelayMs = 300; 
    }
}
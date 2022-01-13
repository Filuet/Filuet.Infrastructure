using Filuet.Infrastructure.Abstractions.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Filuet.Infrastructure.Communication
{
    public class TcpChannel : ICommunicationChannel
    {
        public TcpChannel(string ip, ushort port = 5000)
        {
            _ip = ip;
            _port = port;
            _mutex = new object();
        }

        public byte[] SendCommand(byte[] data, TimeSpan readDelay, TimeSpan timeout, byte? endOfResponse = null)
        {
            lock (_mutex)
            {
                TcpClient client = new TcpClient();
                client.Connect(new IPEndPoint(IPAddress.Parse(_ip), (int)_port));

                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                List<byte> response = new List<byte>();

                Thread.Sleep(readDelay); // Wait for a while before reading (give the slave time to process the command)

                TaskHelpers.ExecuteWithTimeLimit(timeout, () =>
                {
                    TimeSpan newReadDelay = TimeSpan.FromMilliseconds(readDelay.Milliseconds);
                    while (response.Count == 0)
                    {
                        while (stream.CanRead && stream.DataAvailable)
                        {
                            byte nextByte = (byte)stream.ReadByte();
                            response.Add(nextByte);
                            if (endOfResponse.HasValue && nextByte == endOfResponse.Value)
                                break;
                        }
                        newReadDelay += TimeSpan.FromMilliseconds(newReadDelay.Milliseconds * 2); // Slow down the poll as prescripted
                        Thread.Sleep(newReadDelay);
                    }
                });

                client.Close();
                return response.ToArray();
            }
        }

        private readonly string _ip;
        private readonly ushort _port;
        private object _mutex;
    }
}
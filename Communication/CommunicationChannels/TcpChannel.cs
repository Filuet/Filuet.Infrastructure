using Filuet.Infrastructure.Abstractions.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;

namespace Filuet.Infrastructure.Communication
{
    public class TcpChannel : ICommunicationChannel
    {
        public TcpChannel(Action<TcpChannelSettings> channelSetup)
        {
            _settings = channelSetup.CreateTargetAndInvoke();
        }

        public byte[] SendCommand(byte[] data)
        {
            List<byte> result = new List<byte>();

            TcpClient client = new TcpClient();
            client.Connect(_settings.Endpoint);
            client.ReceiveTimeout = (int)_settings.ReceiveTimeout.TotalMilliseconds;

            NetworkStream stream = client.GetStream();

            stream.Write(data, 0, data.Length);

            Thread.Sleep(_settings.ReadDelay);

            if (stream.CanRead)
            {
                byte[] myReadBuffer = new byte[1024];
                int numberOfBytesRead = 0;

                do // Incoming message may be larger than the buffer size
                {
                    numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                    result.AddRange(myReadBuffer.Take(numberOfBytesRead));
                }
                while (stream.DataAvailable);
            }

            client.Close();
            return result.ToArray();
        }

        private readonly TcpChannelSettings _settings;
    }
}
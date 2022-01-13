using System;
using System.Net;

namespace Filuet.Infrastructure.Communication
{
    public class TcpChannelSettings
    {

        public IPEndPoint Endpoint;
        public TimeSpan ReadDelay = TimeSpan.FromMilliseconds(200);
        public TimeSpan ReceiveTimeout = TimeSpan.FromSeconds(20);
    }
}

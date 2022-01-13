using System;
using System.Net;

namespace Filuet.Infrastructure.Communication
{
    public class SerialPortChannelSettings
    {

        public ushort SerialPortNumber;
        public ushort BaudRate;
        public TimeSpan ReadDelay = TimeSpan.FromMilliseconds(200);
        public TimeSpan ReceiveTimeout = TimeSpan.FromSeconds(20);
    }
}

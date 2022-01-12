using System;

namespace Filuet.Infrastructure.Communication
{
    public interface ICommunicationChannel
    {
        byte[] SendCommand(byte[] data, TimeSpan readDelay, TimeSpan timeout, byte? endOfResponse = null);
    }
}
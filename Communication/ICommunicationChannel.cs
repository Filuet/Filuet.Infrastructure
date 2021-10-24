namespace Filuet.Infrastructure.Communication
{
    public interface ICommunicationChannel
    {
        byte[] SendCommand(byte[] data);
    }
}
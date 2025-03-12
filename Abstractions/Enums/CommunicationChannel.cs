using Filuet.Infrastructure.Attributes;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    public enum CommunicationChannel
    {
        [Code("email")]
        Email = 0x01,
        [Code("sms")]
        Sms,
        [Code("telegram")]
        Telegram
    }
}

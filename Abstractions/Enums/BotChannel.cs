using Filuet.Infrastructure.Attributes;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    public enum BotChannel : short
    {
        [Code("emulator")]
        Emulator,
        [Code("skype")]
        Skype,
        [Code("msteams")]
        Teams,
        [Code("telegram")]
        Telegram,
        [Code("webchat")]
        Webchat
    }
}
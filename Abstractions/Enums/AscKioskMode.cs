using Filuet.Infrastructure.Attributes;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    public enum AscKioskMode : int
    {
        [Code("AA")]
        AA = 0x01,
        [Code("AS")]
        AS = 0x02,
        [Code("Mixed")]
        AAAS = 0x03
    }
}
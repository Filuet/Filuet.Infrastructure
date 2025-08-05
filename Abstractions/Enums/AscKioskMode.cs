using Filuet.Infrastructure.Attributes;
using System.ComponentModel;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    public enum AscKioskMode : int
    {
        [Code("INTERNET")]
        [Description("INTERNET")]
        INTERNET = 0x00,
        [Code("AA")]
        [Description("AUTOATTENDANT")]
        AA = 0x01,
        [Code("AS")]
        [Description("AUTOSTORE")]
        AS = 0x02,
        [Code("Mixed")]
        [Description("MIXED")]
        AAAS = 0x03
    }
}
using Filuet.Infrastructure.Attributes;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    public enum Interval
    {
        [Code("day")]
        Day = 0x01,
        [Code("week")]
        week,
        [Code("month")]
        Month,
        [Code("year")]
        Year
    }
}
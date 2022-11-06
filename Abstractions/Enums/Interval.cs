using Filuet.Infrastructure.Attributes;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    public enum Interval : short
    {
        [Code("day")]
        Day = 1,
        [Code("week")]
        week,
        [Code("month")]
        Month,
        [Code("year")]
        Year
    }
}
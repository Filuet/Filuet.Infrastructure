using Filuet.Infrastructure.Attributes;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    public enum CloudPlatform
    {
        [Code("AWS")]
        AWS,
        [Code("Azure")]
        Azure
    }
}
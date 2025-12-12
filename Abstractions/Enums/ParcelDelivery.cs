using Filuet.Infrastructure.Attributes;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    public enum ParcelDelivery
    {
        [Code("DPD")]
        DPD = 0x01,
        [Code("Omniva")]
        Omniva,
        [Code("SmartPosti")]
        SmartPosti
    }
}
using Filuet.Infrastructure.Attributes;
using System.ComponentModel;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    public enum OrderStatus
    {
        [Code("Created")]
        [Description("Created")]
        Created,
        [Code("WaitPayment")]
        [Description("Waiting for payment")]
        WaitingForPayment,
        [Code("Assembling")]
        [Description("Assembling")]
        Assembling,
        [Code("PrepareDelivery")]
        [Description("Preparing for delivery")]
        PreparingForDelivery,
        [Code("Shipment")]
        [Description("Shipment")]
        Shipment,
        [Code("PickUp")]
        [Description("Awaiting to be received")]
        AwaitingToBeReceived,
        [Code("Received")]
        [Description("Received")]
        Received,
        [Code("Cancelled")]
        [Description("Cancelled")]
        Cancelled
    }
}

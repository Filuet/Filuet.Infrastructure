using Filuet.Infrastructure.Attributes;
using System.ComponentModel;

namespace Filuet.Infrastructure.Abstractions.Enums
{
    public enum OrderStatus : int
    {
        [Code("Created")]
        [Description("Created")]
        Created = 0x01,
        [Code("Processing")]
        [Description("Processing")]
        Processing,
        [Code("Completed")]
        [Description("Completed")]
        Completed,
        [Code("Cancelled")]
        [Description("Cancelled")]
        Cancelled,
        [Code("PreOrder")]
        [Description("Pre-ordered")]
        PreOrder,
        [Code("Scanned")]
        [Description("Scanned")]
        Scanned,
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
        Received
    }
}
using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class ShippingDetails
    {
        [JsonPropertyName("type")]
        public ShippingType Type { get; set; } = ShippingType.CourierDelivery;
        [JsonPropertyName("pickup")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PickupDetails Pickup { get; set; } = new PickupDetails();
        [JsonPropertyName("delivery")]
        public DeliveryDetails Delivery { get; set; } = new DeliveryDetails();
        [JsonPropertyName("locker")]
        public ParcelLocker Locker { get; set; } = new ParcelLocker();
        [JsonIgnore]
        public string Tag
            => FluentSwitch.On(Type).Case(ShippingType.CourierDelivery).Then(ShippingType.CourierDelivery.GetCode())
                .Case(ShippingType.Store).Then($"{ShippingType.Store.GetCode()}:{Pickup.StoreCode}")
                .Default(null);

        [JsonIgnore]
        public bool IsSufficient
            => FluentSwitch.On(Type).Case(ShippingType.CourierDelivery).Then(Delivery.IsSufficient).
            Case(ShippingType.Store).Then(Pickup.IsSufficient).Default(false);

        public override string ToString()
            => FluentSwitch.On(Type).Case(ShippingType.CourierDelivery).Then(Delivery.ToString()).
            Case(ShippingType.Store).Then(Pickup.StoreCode).Default(null);
    }
}
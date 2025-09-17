using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class ShippingDetails
    {
        [JsonPropertyName("type")]
        public ShippingType Type { get; set; } = ShippingType.CourierDelivery;
        [JsonPropertyName("storeCode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string StoreCode { get; set; }
        [JsonPropertyName("pickUpPointCode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string PickUpPointCode { get; set; }
        [JsonPropertyName("delivery")]
        public DeliveryDetails Delivery { get; set; } = new DeliveryDetails();
        [JsonIgnore]
        public string Tag
            => FluentSwitch.On(Type).Case(ShippingType.CourierDelivery).Then(ShippingType.CourierDelivery.GetCode())
                .Case(ShippingType.Store).Then($"{ShippingType.Store.GetCode()}:{StoreCode}")
                .Case(ShippingType.PickUpPoint).Then($"{ShippingType.PickUpPoint.GetCode()}:{PickUpPointCode}")
                .Default(null);
        [JsonIgnore]
        public bool IsSufficient
            => FluentSwitch.On(Type).Case(ShippingType.CourierDelivery).Then(Delivery.IsSufficient).
            Case(ShippingType.Store).Then(!string.IsNullOrWhiteSpace(StoreCode)).
            Case(ShippingType.PickUpPoint).Then(!string.IsNullOrWhiteSpace(PickUpPointCode)).Default(false);

        public override string ToString()
            => FluentSwitch.On(Type).Case(ShippingType.CourierDelivery).Then(Delivery.ToString()).
            Case(ShippingType.Store).Then(StoreCode).
            Case(ShippingType.PickUpPoint).Then(PickUpPointCode).Default(null);
    }
}
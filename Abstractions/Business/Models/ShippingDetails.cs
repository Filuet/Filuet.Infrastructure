using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class ShippingDetails
    {
        [JsonPropertyName("type")]
        public ShippingType Type { get; set; } = ShippingType.CourierDelivery;
        public string StoreCode { get; set; }
        public string PickUpPointCode { get; set; }
        public DeliveryDetails Delivery { get; set; }

        public override string ToString()
            => FluentSwitch.On(Type).Case(ShippingType.CourierDelivery).Then(Delivery.ToString()).
            Case(ShippingType.Store).Then(StoreCode).
            Case(ShippingType.PickUpPoint).Then(PickUpPointCode).Default(null);
    }
}
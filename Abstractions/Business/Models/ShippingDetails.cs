using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System.Linq;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class ShippingDetails
    {
        [JsonPropertyName("type")]
        public ShippingType Type { get; set; } = ShippingType.CourierDelivery;
        [JsonPropertyName("store")]
        public StoreDetails Store { get; set; } = new StoreDetails();
        [JsonPropertyName("delivery")]
        public DeliveryDetails Delivery { get; set; } = new DeliveryDetails();
        [JsonPropertyName("locker")]
        public LockerDetails Locker { get; set; } = new LockerDetails();
        [JsonPropertyName("pickupPoint")]
        public PickupPointDetails PickupPoint { get; set; } = new PickupPointDetails();
        [JsonPropertyName("comment")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Comment { get; set; }
        [JsonIgnore]
        public string Tag
            => FluentSwitch.On(Type).Case(ShippingType.CourierDelivery).Then(ShippingType.CourierDelivery.GetCode())
                .Case(ShippingType.Store).Then($"{ShippingType.Store.GetCode()}:{Store.StoreCode}")
                .Default(null);

        [JsonIgnore]
        public bool IsSufficient
            => FluentSwitch.On(Type).Case(ShippingType.CourierDelivery).Then(Delivery.IsSufficient)
                .Case(ShippingType.Store).Then(Store.IsSufficient)
                .Case(ShippingType.Locker).Then(Locker.IsSufficient)
                .Case(ShippingType.PickupPoint).Then(PickupPoint.IsSufficient)
            .Default(false);

        /// <summary>
        /// Leave only the shipping options that math the available types. Available types vary from country to country and depends on circumstances
        /// </summary>
        /// <param name="availableTypes"></param>
        /// <returns></returns>
        public ShippingDetails Filter(ShippingType[] availableTypes) {
            if (availableTypes == null || !availableTypes.Any()) {
                Store = null;
                Delivery = null;
                Locker = null;
                PickupPoint = null;

                return this;
            }

            if (!availableTypes.Contains(ShippingType.Store))
                Store = null;

            if (!availableTypes.Contains(ShippingType.CourierDelivery))
                Delivery = null;

            if (!availableTypes.Contains(ShippingType.Locker))
                Locker = null;

            if (!availableTypes.Contains(ShippingType.PickupPoint))
                PickupPoint = null;

            return this;
        }

        public override string ToString()
            => FluentSwitch.On(Type).Case(ShippingType.CourierDelivery).Then(Delivery.ToString())
                .Case(ShippingType.Store).Then(Store.StoreCode)
                .Case(ShippingType.PickupPoint).Then(PickupPoint.Point.Code)
            .Default(null);
    }
}
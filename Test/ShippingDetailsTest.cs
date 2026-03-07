using Filuet.Infrastructure.Abstractions.Business.Models;
using Filuet.Infrastructure.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class ShippingDetailsTest
    {

        [Fact]
        public void Test_Tag_success() {
            // Prepare
            ShippingDetails details = new ShippingDetails() {
                Type = ShippingType.PickupPoint,
                Delivery = new DeliveryDetails { Address = new Address { PostCode = "100001", Country = Country.Latvia, AddressLine = "FooBarBaz", City = "Toshkent" }, Recipient = "bar", MobileNumber = "153333333", Invoice = true },
                PickupPoint = new PickupPointDetails { Invoice = false, MobileNumber = "000988977", Recipient = "bar", Point = new PickupPoint { Code = "7",  Address = "Temur 13", City = "Andijan city", Country = Country.Uzbekistan, FreightCode = "UPO", WarehouseCode = "UZ", ServiceName = "3PL AMB" } },
                Comment = string.Empty
            };

            details.Store = null;

            // Perform
            string tag = details.Tag;
        }
    }
}

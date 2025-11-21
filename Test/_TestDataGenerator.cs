using Filuet.Infrastructure.Abstractions.Business.Models;
using System.Collections.Generic;

namespace Test
{
    public class TestDataGenerator
    {
        static public IEnumerable<object[]> GetOrderDTOs() {
            yield return new object[] { "{\"number\":\"LRK1441803\",\"customer\":\"U521030158\",\"name\":\"DREIBLATHENA AGNITA\",\"country\":\"LV\",\"language\":\"en\",\"date\":\"2022-04-19T18:55:32\",\"points\":23.95,\"extra\":{\"Order.Warehouse\":\"LR\",\"Customer.Name\":\"AGNITA\",\"Customer.SurName\":\"DREIBLATHENA\",\"Kiosk\":\"LVRIGAA2\",\"SelectedMonth\":\"4/1/2022 12:00:00 AM\",\"Customer.Carrier\":\"print\",\"Customer.Destination\":\"\",\"Customer.Discount\":\"35\",\"Customer.InvShipFlag\":\"Y\",\"Payment.TotalAmount\":\"22.39\",\"Payment.TotalTax\":\"4.7\",\"Payment.TotalOtherCharges\":\"0.0\",\"Payment.TotalDiscountAmount\":\"9.79\",\"Payment.TotalRetailAmount\":\"30.68\",\"Payment.TotalOrderAmount\":\"22.39\",\"Payment.TotalFreightCharges\":\"1.5\",\"Payment.TotalPkgHandling\":\"0.0\",\"Payment.TotalLogisticCharges\":\"0.0\",\"Payment.Currency\":\"\",\"Payment.TermID\":\"\",\"Payment.TransDate\":\"\",\"Payment.TransTime\":\"\",\"Payment.PaymentType\":\"None\",\"Payment.Amount\":\"0.00\",\"Payment.AuthCode\":\"\",\"Payment.CardNumber\":\"\",\"Payment.CardHolderName\":\"\",\"Payment.CardType\":\"\",\"Payment.Installment\":\"\",\"Payment.TotalPaid\":\"0\",\"Payment.IsPartialPaid\":\"false\",\"Payment.ChangeAmount\":\"0\"},\"obtainMethod\":\"WH\",\"paymentMethod\":\"cash\",\"total\":{\"value\":27.09,\"curr\":\"EUR\"},\"paid\":{\"value\":0,\"curr\":\"EUR\"},\"items\":[{\"name\":\"Formula 1 - Spiced apple\",\"dueAmount\":{\"value\":27.0872,\"curr\":\"EUR\"},\"totalAmount\":{\"value\":22.3872,\"curr\":\"EUR\"},\"points\":23.95,\"uid\":\"4464\",\"qty\":1}],\"uncollected\":null}" };
        }


        static public IEnumerable<object[]> GetWorkingHours() {
            //yield return new object[] { "{\"Mon\":[{\"from\":\"00:00\",\"to\":\"23:59\",\"mode\":\"AS\"}],\"Tue\":[{\"from\":\"00:00\",\"to\":\"21:59\",\"mode\":\"AS\"},{\"from\":\"22:59\",\"to\":\"23:59\",\"mode\":\"AAAS\"}],\"Wed\":[{\"from\":\"00:00\",\"to\":\"23:59\",\"mode\":\"AS\"}],\"Thu\":[{\"from\":\"00:00\",\"to\":\"23:59\",\"mode\":\"AS\"}],\"Fri\":[{\"from\":\"00:00\",\"to\":\"23:59\",\"mode\":\"AS\"}],\"Sat\":[{\"from\":\"00:00\",\"to\":\"23:59\",\"mode\":\"AS\"}],\"Sun\":[{\"from\":\"00:00\",\"to\":\"23:59\",\"mode\":\"AS\"}]}" };
            yield return new object[] { "{\"Mon\":[{\"from\":\"00:00\",\"to\":\"23:59\",\"mode\":\"AA\"}],\"Tue\":[{\"from\":\"00:00\",\"to\":\"17:59\",\"mode\":\"AA\"}],\"Wed\":[{\"from\":\"00:00\",\"to\":\"23:59\",\"mode\":\"AA\"}],\"Thu\":[{\"from\":\"00:00\",\"to\":\"23:59\",\"mode\":\"AA\"}],\"Fri\":[{\"from\":\"00:00\",\"to\":\"23:59\",\"mode\":\"AA\"}],\"Sat\":[],\"Sun\":[{\"from\":\"00:00\",\"to\":\"23:59\",\"mode\":\"AA\"}]}" };
        }

        static public IEnumerable<object[]> GetCashCrashOrderDTOs() {
            //yield return new object[] { "{\"number\":\"LRK1441803\",\"customer\":\"U521030158\",\"name\":\"DREIBLATHENA AGNITA\",\"country\":\"LV\",\"language\":\"en\",\"date\":\"2022-04-19T18:55:32\",\"points\":23.95,\"extra\":{\"Order.Warehouse\":\"LR\",\"Customer.Name\":\"AGNITA\",\"Customer.SurName\":\"DREIBLATHENA\",\"Kiosk\":\"LVRIGAA2\",\"SelectedMonth\":\"4/1/2022 12:00:00 AM\",\"Customer.Carrier\":\"print\",\"Customer.Destination\":\"\",\"Customer.Discount\":\"35\",\"Customer.InvShipFlag\":\"Y\",\"Payment.TotalAmount\":\"22.39\",\"Payment.TotalTax\":\"4.7\",\"Payment.TotalOtherCharges\":\"0.0\",\"Payment.TotalDiscountAmount\":\"9.79\",\"Payment.TotalRetailAmount\":\"30.68\",\"Payment.TotalOrderAmount\":\"22.39\",\"Payment.TotalFreightCharges\":\"1.5\",\"Payment.TotalPkgHandling\":\"0.0\",\"Payment.TotalLogisticCharges\":\"0.0\",\"Payment.Currency\":\"\",\"Payment.TermID\":\"\",\"Payment.TransDate\":\"\",\"Payment.TransTime\":\"\",\"Payment.PaymentType\":\"None\",\"Payment.Amount\":\"0.00\",\"Payment.AuthCode\":\"\",\"Payment.CardNumber\":\"\",\"Payment.CardHolderName\":\"\",\"Payment.CardType\":\"\",\"Payment.Installment\":\"\",\"Payment.TotalPaid\":\"0\",\"Payment.IsPartialPaid\":\"false\",\"Payment.ChangeAmount\":\"0\"},\"obtainMethod\":\"WH\",\"paymentMethod\":\"cash\",\"total\":{\"value\":27.09,\"curr\":\"EUR\"},\"paid\":{\"value\":30.0,\"curr\":\"EUR\"},\"change\":{\"value\":2.91,\"curr\":\"EUR\"},\"items\":[{\"name\":\"Formula 1 - Spiced apple\",\"dueAmount\":{\"value\":27.0872,\"curr\":\"EUR\"},\"totalAmount\":{\"value\":22.3872,\"curr\":\"EUR\"},\"points\":23.95,\"uid\":\"4464\",\"qty\":1}],\"uncollected\":null}" };
            yield return new object[] { "{\"number\":\"LRK1441803\",\"customer\":\"U521030158\",\"name\":\"DREIBLATHENA AGNITA\",\"country\":\"LV\",\"language\":\"en\",\"date\":\"2022-04-19T18:55:32\",\"points\":23.95,\"extra\":{\"Order.Warehouse\":\"LR\",\"Customer.Name\":\"AGNITA\",\"Customer.SurName\":\"DREIBLATHENA\",\"Kiosk\":\"LVRIGAA2\",\"SelectedMonth\":\"4/1/2022 12:00:00 AM\",\"Customer.Carrier\":\"print\",\"Customer.Destination\":\"\",\"Customer.Discount\":\"35\",\"Customer.InvShipFlag\":\"Y\",\"Payment.TotalAmount\":\"22.39\",\"Payment.TotalTax\":\"4.7\",\"Payment.TotalOtherCharges\":\"0.0\",\"Payment.TotalDiscountAmount\":\"9.79\",\"Payment.TotalRetailAmount\":\"30.68\",\"Payment.TotalOrderAmount\":\"22.39\",\"Payment.TotalFreightCharges\":\"1.5\",\"Payment.TotalPkgHandling\":\"0.0\",\"Payment.TotalLogisticCharges\":\"0.0\",\"Payment.Currency\":\"\",\"Payment.TermID\":\"\",\"Payment.TransDate\":\"\",\"Payment.TransTime\":\"\",\"Payment.PaymentType\":\"None\",\"Payment.Amount\":\"0.00\",\"Payment.AuthCode\":\"\",\"Payment.CardNumber\":\"\",\"Payment.CardHolderName\":\"\",\"Payment.CardType\":\"\",\"Payment.Installment\":\"\",\"Payment.TotalPaid\":\"0\",\"Payment.IsPartialPaid\":\"false\",\"Payment.ChangeAmount\":\"0\"},\"obtainMethod\":\"WH\",\"paymentMethod\":\"cash\",\"total\":{\"value\":27.09,\"curr\":\"EUR\"},\"paid\":{\"value\":10.0,\"curr\":\"EUR\"},\"change\":{\"value\":0,\"curr\":\"EUR\"},\"items\":[{\"name\":\"Formula 1 - Spiced apple\",\"dueAmount\":{\"value\":27.0872,\"curr\":\"EUR\"},\"totalAmount\":{\"value\":22.3872,\"curr\":\"EUR\"},\"points\":23.95,\"uid\":\"4464\",\"qty\":1}],\"uncollected\":null}" };
        }

        static public IEnumerable<object[]> GetCartDiff() {
            // something to add, something to remove
            yield return new object[] { Cart.Create([ CartItem.Create("aaa", 1), CartItem.Create("bbb", 3)]),
                Cart.Create([CartItem.Create("aaa", 2), CartItem.Create("bbb", 1), CartItem.Create("ccc", 2)]),
                new List<CartItem>([CartItem.Create("bbb", 2)]),
                new List<CartItem>([CartItem.Create("aaa", 1), CartItem.Create("ccc", 2)])
            };

            // clean cart
            yield return new object[] { Cart.Create(null),
                Cart.Create([CartItem.Create("aaa", 1), CartItem.Create("bbb", 1)]),
                new List<CartItem>(),
                new List<CartItem>([CartItem.Create("aaa", 1), CartItem.Create("bbb", 1)])
            };

            // add new cart
            yield return new object[] { Cart.Create([CartItem.Create("aaa", 1), CartItem.Create("bbb", 1)]),
                Cart.Create(null),
                new List<CartItem>([CartItem.Create("aaa", 1), CartItem.Create("bbb", 1)]),
                new List<CartItem>()
            };
        }
        static public IEnumerable<object[]> GetCarts() {
            yield return new object[] { Cart.Create([CartItem.Create("foo", 1), CartItem.Create("bar", 3)]) };
            yield return new object[] { Cart.Create([CartItem.Create("baz", 1)]) };
            yield return new object[] { Cart.Create([CartItem.Create("quz", 1), null]) };
        }
    }
}
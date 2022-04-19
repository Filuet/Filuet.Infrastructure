using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Test
{
    public class TestDataGenerator
    {
        static public IEnumerable<object[]> GetOrderDTOs()
        {
            yield return new object[] { "{\"number\":\"LRK1441803\",\"customer\":\"U521030158\",\"name\":\"DREIBLATHENA AGNITA\",\"country\":\"LV\",\"language\":\"en\",\"date\":\"2022-04-19T18:55:32\",\"points\":23.95,\"extra\":{\"Order.Warehouse\":\"LR\",\"Customer.Name\":\"AGNITA\",\"Customer.SurName\":\"DREIBLATHENA\",\"Kiosk\":\"LVRIGAA2\",\"SelectedMonth\":\"4/1/2022 12:00:00 AM\",\"Customer.Carrier\":\"print\",\"Customer.Destination\":\"\",\"Customer.Discount\":\"35\",\"Customer.InvShipFlag\":\"Y\",\"Payment.TotalAmount\":\"22.39\",\"Payment.TotalTax\":\"4.7\",\"Payment.TotalOtherCharges\":\"0.0\",\"Payment.TotalDiscountAmount\":\"9.79\",\"Payment.TotalRetailAmount\":\"30.68\",\"Payment.TotalOrderAmount\":\"22.39\",\"Payment.TotalFreightCharges\":\"1.5\",\"Payment.TotalPkgHandling\":\"0.0\",\"Payment.TotalLogisticCharges\":\"0.0\",\"Payment.Currency\":\"\",\"Payment.TermID\":\"\",\"Payment.TransDate\":\"\",\"Payment.TransTime\":\"\",\"Payment.PaymentType\":\"None\",\"Payment.Amount\":\"0.00\",\"Payment.AuthCode\":\"\",\"Payment.CardNumber\":\"\",\"Payment.CardHolderName\":\"\",\"Payment.CardType\":\"\",\"Payment.Installment\":\"\",\"Payment.TotalPaid\":\"0\",\"Payment.IsPartialPaid\":\"false\",\"Payment.ChangeAmount\":\"0\"},\"obtainMethod\":\"WH\",\"paymentMethod\":\"cash\",\"total\":{\"value\":27.09,\"curr\":\"EUR\"},\"paid\":{\"value\":0,\"curr\":\"EUR\"},\"items\":[{\"name\":\"Formula 1 - Spiced apple\",\"dueAmount\":{\"value\":27.0872,\"curr\":\"EUR\"},\"totalAmount\":{\"value\":22.3872,\"curr\":\"EUR\"},\"points\":23.95,\"uid\":\"4464\",\"qty\":1}],\"uncollected\":null}" };
        }
    }
}

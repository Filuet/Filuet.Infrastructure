using Filuet.Infrastructure.Abstractions.Business;
using Filuet.Infrastructure.Ordering.Dto;
using Filuet.Infrastructure.Ordering.Helpers;
using Filuet.Infrastructure.Ordering.Models;
using System.Text.Json;
using Xunit;

namespace Test
{
    public class OrderTest
    {
        [Theory]
        [MemberData(nameof(TestDataGenerator.GetOrderDTOs), MemberType = typeof(TestDataGenerator))]
        public void Test_OrderDto_to_Order(string dto)
        {
            // Prepare
            OrderDto orderDto = JsonSerializer.Deserialize<OrderDto>(dto);
            // Pre-validate

            // Perform
            Order model = orderDto.ToModel();

            // Post-validate
           // Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_Order_from_Json()
        {
            // Prepare
            string data = "{\r\n  \"isCrash\": false,\r\n  \"number\": \"16K1498227\",\r\n  \"customer\": \"VA00248957\",\r\n  \"name\": \"LÊ THỊ TRANG\",\r\n  \"country\": \"IL\",\r\n  \"language\": \"en\",\r\n  \"date\": \"2023-09-25T16:39:53.6198936+03:00\",\r\n  \"points\": 23.95,\r\n  \"extra\": {\r\n    \"Tin\": \"\",\r\n    \"Kiosk\": \"ILTELAA1\",\r\n    \"HeaderLines\": \"הרבלייף אינטרנשיונל לישראל (1990) בע\\\"מ\\\\r\\\\nדרך המכבים 46, אזה\\\"ת הישן ראשל\\\"צ 75359\\\\r\\\\nCall-In Orders, Tel. 03-9431133, Fax 03-9431130\\\\r\\\\nShipping & Warehouse Inquiries,\\\\r\\\\nTel. 03-9431161/72, Fax 03-9431160\",\r\n    \"SelectedMonth\": \"2023/09\",\r\n    \"Installments\": \"\",\r\n    \"DSType\": \"SP\",\r\n    \"FreightCharges\": \"0\",\r\n    \"PackageAndHandlingCharges\": \"0\",\r\n    \"OrderAmountBeforeVAT\": \"98.55\",\r\n    \"Discount\": \"98.55\",\r\n    \"TotalRetail\": \"182.16\",\r\n    \"AuthCode\": \"b22b0177-cb51-4f5b-a776-a4ac7ffd382d\",\r\n    \"CardNo\": \"************0275\",\r\n    \"VAT\": \"16.76\",\r\n    \"PaidNative\": \"\"\r\n  },\r\n  \"obtainMethod\": \"WH\",\r\n  \"paymentMethod\": \"Card\",\r\n  \"installments\": null,\r\n  \"total\": {\r\n    \"Value\": 115.31,\r\n    \"Currency\": \"ILS\"\r\n  },\r\n  \"paid\": {\r\n    \"Value\": 115.31,\r\n    \"Currency\": \"ILS\"\r\n  },\r\n  \"change\": {\r\n    \"Value\": 0,\r\n    \"Currency\": \"ILS\"\r\n  },\r\n  \"changeGiven\": {\r\n    \"Value\": 0,\r\n    \"Currency\": \"ILS\"\r\n  },\r\n  \"items\": [\r\n    {\r\n      \"name\": \"PROTEIN DRINK FORMULA 1 VANILLA FLAVOR\",\r\n      \"dueAmount\": {\r\n        \"Value\": 115.31,\r\n        \"Currency\": \"ILS\"\r\n      },\r\n      \"totalAmount\": {\r\n        \"Value\": 115.31,\r\n        \"Currency\": \"ILS\"\r\n      },\r\n      \"points\": 23.95,\r\n      \"uid\": \"0141\",\r\n      \"qty\": 1\r\n    }\r\n  ],\r\n  \"uncollected\": []\r\n}";

            // Pre-validate

            // Perform
            Order order = JsonSerializer.Deserialize<Order>(data);

            // Post-validate
            // Assert.Equal(expected, actual);
        }
    }
}
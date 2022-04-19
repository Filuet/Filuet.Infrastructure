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
    }
}

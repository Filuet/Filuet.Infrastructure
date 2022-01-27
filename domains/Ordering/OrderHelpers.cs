using Filuet.Infrastructure.Abstractions.Helpers;
using Filuet.Infrastructure.Ordering.Dto;
using Filuet.Infrastructure.Ordering.Models;
using System.Linq;

namespace Filuet.Infrastructure.Ordering.Helpers
{
    public static class OrderHelpers
    {
        public static OrderDto ToDto(this Order order)
        {
            if (order == null)
                return null;

            return new OrderDto
            {
                Number = order.Number,
                Customer = order.Customer,
                CustomerName = order.CustomerName,
                CountryCode = order.Location.GetCode(),
                LanguageCode = order.Language.GetCode(),
                Date = order.Date,
                Points = order.Points,
                ExtraData = order.ExtraData,
                Obtaining = order.Obtaining.GetCode(),
                Amount = new MoneyDto { Value = order.Total.Value, Currency = order.Total.Currency.GetCode() },
                Paid = new MoneyDto { Value = order.Paid.Value, Currency = order.Paid.Currency.GetCode() },
                Items = order.Items?.Select(x => new OrderLineDto
                {
                    ProductUID = x.ProductUID,
                    Name = x.Name,
                    Points = x.Points,
                    Quantity = x.Quantity,
                    Amount = new MoneyDto { Value = x.Amount.Value, Currency = x.Amount.Currency.GetCode() }
                }),
                UncollectedItems = order.UncollectedItems?.Select(x => new OrderItemDto {
                    ProductUID = x.ProductUID,
                    Quantity = x.Quantity 
                })
            };
        }
    }
}

using Filuet.Infrastructure.Abstractions.Business;
using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using Filuet.Infrastructure.Ordering.Builders;
using Filuet.Infrastructure.Ordering.Dto;
using Filuet.Infrastructure.Ordering.Enums;
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

        public static Order ToModel(this OrderDto dto)
        {
            OrderBuilder b = new OrderBuilder()
                .WithHeader(dto.Number, dto.Date, dto.Customer, dto.CustomerName, EnumHelpers.GetValueFromCode<Country>(dto.CountryCode), EnumHelpers.GetValueFromCode<Language>(dto.LanguageCode))
                .WithObtainingMethod(EnumHelpers.GetValueFromCode<GoodsObtainingMethod>(dto.Obtaining))
                .WithTotalValues(Money.Create(dto.Amount.Value, EnumHelpers.GetValueFromCode<Currency>(dto.Amount.Currency)),
                    Money.Create(dto.Paid.Value, EnumHelpers.GetValueFromCode<Currency>(dto.Paid.Currency)), dto.Points)
                .WithItems(dto.Items.Select(x => new OrderLine
                {
                    ProductUID = x.ProductUID,
                    Name = x.Name,
                    TotalAmount = Money.Create(x.TotalAmount.Value, EnumHelpers.GetValueFromCode<Currency>(x.TotalAmount.Currency)),
                    Amount = Money.Create(x.Amount.Value, EnumHelpers.GetValueFromCode<Currency>(x.Amount.Currency)),
                    Quantity = x.Quantity,
                    Points = x.Points
                }).ToArray())
                .WithUncollectedItems(dto.UncollectedItems.Select(x => new OrderItem { ProductUID = x.ProductUID, Quantity = x.Quantity }));

            foreach (var e in dto.ExtraData)
                b.WithExtraData(e.Key, e.Value);

            return b.Build();
        }
    }
}

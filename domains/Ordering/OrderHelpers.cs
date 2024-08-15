using Filuet.Infrastructure.Abstractions.Business;
using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using Filuet.Infrastructure.Ordering.Builders;
using Filuet.Infrastructure.Ordering.Dto;
using Filuet.Infrastructure.Ordering.Enums;
using Filuet.Infrastructure.Ordering.Models;
using System;
using System.Linq;

namespace Filuet.Infrastructure.Ordering.Helpers
{
    public static class OrderHelpers
    {
        public static OrderDto ToDto(this Order order)
        {
            if (order == null)
                return null;

            OrderDto result = new OrderDto {
                Number = order.Number,
                Customer = order.Customer,
                CustomerName = order.CustomerName,
                CountryCode = order.Location.GetCode(),
                LanguageCode = order.Language.GetCode(),
                Date = order.Date,
                Points = order.Points,
                ExtraData = order.ExtraData,
                Obtaining = order.Obtaining.GetCode(),
                Amount = new Money { Value = order.Total.Value, Currency = order.Total.Currency },
                Paid = new Money { Value = order.Paid.Value, Currency = order.Paid.Currency },
                Items = order.Items?.Select(x => new OrderLineDto {
                    ProductUID = x.ProductUID,
                    Name = x.Name,
                    Points = x.Points,
                    Quantity = x.Quantity,
                    DueAmount = new Money { Value = x.DueAmount.Value, Currency = x.DueAmount.Currency },
                    TotalAmount = new Money { Value = x.TotalAmount.Value, Currency = x.TotalAmount.Currency },
                }),
                UncollectedItems = order.UncollectedItems?.Select(x => new OrderItemDto {
                    ProductUID = x.ProductUID,
                    Quantity = x.Quantity 
                }),
                PaymentMethod = order.PaymentMethod?.GetCode(),
                Change = order.Change,
                ChangeGiven = order.ChangeGiven 
            };

            result.IsCrash = (result.UncollectedItems != null && result.UncollectedItems.Any()) // Not given items
                || result.Paid.Value < result.Amount.Value  // Underpaid
                || result.Change?.Value > result.ChangeGiven?.Value; // Change not given

            return result;
        }

        public static Order ToModel(this OrderDto dto)
        {
            Currency baseCurrency = dto.Amount.Currency;
            Func<Money, Money> _withDefaultMoney = x => {
                if (x == null )
                    return Money.Create(0m, baseCurrency);

                return x;
            };

            OrderBuilder b = new OrderBuilder()
                .WithHeader(dto.Number, dto.Date, dto.Customer, dto.CustomerName, EnumHelpers.GetValueFromCode<Country>(dto.CountryCode), EnumHelpers.GetValueFromCode<Language>(dto.LanguageCode), dto.IsCrash)
                .WithObtainingMethod(EnumHelpers.GetValueFromCode<GoodsObtainingMethod>(dto.Obtaining))
                .WithPaymentMethod(dto.PaymentMethod)
                .WithTotalValues(Money.Create(dto.Amount.Value, baseCurrency),
                    _withDefaultMoney(dto.Paid),
                    _withDefaultMoney(dto.Change),
                    _withDefaultMoney(dto.ChangeGiven),
                    dto.Points)
                .WithItems(dto.Items.Select(x => new OrderLine {
                    ProductUID = x.ProductUID,
                    Name = x.Name,
                    TotalAmount = _withDefaultMoney(x.TotalAmount),
                    DueAmount = _withDefaultMoney(x.DueAmount),
                    Quantity = x.Quantity,
                    Points = x.Points
                }).ToArray())
                .WithUncollectedItems(dto.UncollectedItems?.Select(x => new OrderItem { ProductUID = x.ProductUID, Quantity = x.Quantity }))
                .WithInstallmentPayments(dto.Installments);

            foreach (var e in dto.ExtraData)
                b.WithExtraData(e.Key, e.Value);

            return b.Build();
        }
    }
}
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
                    DueAmount = new MoneyDto { Value = x.DueAmount.Value, Currency = x.DueAmount.Currency.GetCode() },
                    TotalAmount = new MoneyDto { Value = x.TotalAmount.Value, Currency = x.TotalAmount.Currency.GetCode() },
                }),
                UncollectedItems = order.UncollectedItems?.Select(x => new OrderItemDto {
                    ProductUID = x.ProductUID,
                    Quantity = x.Quantity 
                }),
                PaymentMethod = order.PaymentMethod?.GetCode()
            };
        }

        public static Order ToModel(this OrderDto dto)
        {
            Currency baseCurrency = EnumHelpers.GetValueFromCode<Currency>(dto.Amount.Currency);
            Func<MoneyDto, Money> _withDefaultMoney = (moneyDto) =>
            {
                if (moneyDto == null || string.IsNullOrWhiteSpace(moneyDto.Currency))
                    return Money.Create(0m, baseCurrency);

                return Money.Create(moneyDto.Value, EnumHelpers.GetValueFromCode<Currency>(moneyDto.Currency));
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
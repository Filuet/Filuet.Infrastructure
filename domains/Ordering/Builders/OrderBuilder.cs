using Filuet.Infrastructure.Abstractions.Business;
using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Ordering.Enums;
using Filuet.Infrastructure.Ordering.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Filuet.Infrastructure.Ordering.Builders
{
    public class OrderBuilder
    {
        private IEnumerable<OrderLine> _items;
        private IEnumerable<OrderItem> _uncollectedItems;
        private Money _amount;
        private Money _paid;
        private string _orderNumber;
        private DateTime _orderDate;
        private string _customer;
        private string _customerName;
        private Country _locale;
        private Language _language;
        private decimal _points;
        private GoodsObtainingMethod _method;
        private Dictionary<string, object> _extraData = new Dictionary<string, object>();

        public OrderBuilder WithObtainingMethod(GoodsObtainingMethod method)
        {
            _method = method;
            return this;
        }

        public OrderBuilder WithHeader(string orderNumber, DateTime date, string customer, string customerName, Country locale, Language language)
        {
            if (string.IsNullOrWhiteSpace(orderNumber) || orderNumber.Trim().Length < 4)
                throw new ArgumentException("Order number is mandatory");

            if (date == DateTime.MinValue || date <= DateTime.Now.AddHours(-1))
                throw new ArgumentException("Invalid order date");

            if (string.IsNullOrWhiteSpace(customer) || customer.Trim().Length < 4)
                throw new ArgumentException("Customer is mandatory");

            if (string.IsNullOrWhiteSpace(customerName) || customerName.Trim().Length < 2)
                throw new ArgumentException("Customer name is mandatory");

            _orderNumber = orderNumber.Trim();
            _orderDate = date;
            _customer = customer.Trim();
            _customerName = customerName.Trim().ToUpper();
            _locale = locale;
            _language = language;

            return this;
        }

        public OrderBuilder WithItems(params OrderLine[] items)
        {
            if (items == null || !items.Any())
                throw new ArgumentException("Items must be specified");

            if (items.GroupBy(x => x.ProductUID).Any(x => x.Count() > 1))
                throw new ArgumentException("Duplicates founded in order items");

            if (items.Any(x => x.Amount == null || x.Amount.Value < 0)) // An item could costs 0 (a gift for example)
                throw new ArgumentException("Invalid item(s) detected");

            if (items.GroupBy(x => x.Amount.Currency).Select(x => x.Key).Distinct().Count() > 1)
                throw new ArgumentException("Multiply currencies have been detected in order items");

            _items = items;

            Currency itemsCurrency = items.GroupBy(x => x.Amount.Currency).Select(x => x.Key).Distinct().First();

            if (_amount != null && (Math.Abs(items.Sum(x => x.Amount.Value) - _amount.Value) >= 1m || _amount.Currency != itemsCurrency))
                throw new ArgumentException("Order amount is not equals to order items summ or order currency different from order items");

            return this;
        }

        public OrderBuilder WithTotalValues(Money amount, Money paid, decimal points = 0)
        {
            if (amount.Value < 0)
                throw new ArgumentException("Order amount must be positive");

            if (_items != null)
            {
                Currency itemsCurrency = _items.GroupBy(x => x.Amount.Currency).Select(x => x.Key).Distinct().First();

                if (amount != null && (Math.Abs(_items.Sum(x => x.Amount.Value) - amount.Value) >= 1m || amount.Currency != itemsCurrency))
                    throw new ArgumentException("Order amount is not equals to order items summ or order currency different from order items");
            }

            _amount = amount;
            _paid = paid ?? _amount;
            _points = points;

            return this;
        }

        public OrderBuilder WithExtraData(string name, object value)
        {
            if (!string.IsNullOrWhiteSpace(name))
                _extraData[name.Trim()] = value == null ? string.Empty : value.ToString();

            return this;
        }

        public OrderBuilder WithUncollectedItems(IEnumerable<OrderItem> items)
        {
            _uncollectedItems = items ?? new List<OrderItem>();

            return this;
        }

        public Order Build()
        {
            if (_items == null || _items.Count() == 0)
                throw new ArgumentException("Items must be specified");

            if (_amount.Value < 0)
                throw new ArgumentException("Order amount must be positive");

            if (string.IsNullOrWhiteSpace(_orderNumber) || _orderNumber.Trim().Length < 4)
                throw new ArgumentException("Order number is mandatory");

            if (string.IsNullOrWhiteSpace(_customer) || _customer.Trim().Length < 4)
                throw new ArgumentException("Customer is mandatory");

            return new Order
            {
                Items = _items,
                UncollectedItems = _uncollectedItems,
                Amount = _amount,
                Paid = _paid,
                Customer = _customer,
                CustomerName = _customerName,
                Points = _points,
                Location = _locale,
                Language = _language,
                Number = _orderNumber,
                Date = _orderDate,
                Obtaining = _method,
                ExtraData = _extraData
            };
        }
    }
}

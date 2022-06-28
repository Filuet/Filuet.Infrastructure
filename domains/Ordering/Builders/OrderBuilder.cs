using Filuet.Infrastructure.Abstractions.Business;
using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
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
        private Money _total;
        private Money _paid;
        private Money _change;
        private Money _changeGiven;
        private string _orderNumber;
        private DateTime _orderDate;
        private string _customer;
        private string _customerName;
        private Country _locale;
        private Language _language;
        private decimal _points;
        private GoodsObtainingMethod _obtainingMethod;
        private PaymentMethod? _paymentMethod;
        private uint? _installmentPayments;
        private Dictionary<string, object> _extraData = new Dictionary<string, object>();

        public OrderBuilder WithObtainingMethod(GoodsObtainingMethod method)
        {
            _obtainingMethod = method;
            return this;
        }

        public OrderBuilder WithPaymentMethod(string method)
        {
            if (string.IsNullOrWhiteSpace(method))
                _paymentMethod = null;
            else _paymentMethod = EnumHelpers.GetValueFromCode<PaymentMethod>(method);

            return this;
        }

        public OrderBuilder WithHeader(string orderNumber, DateTime date, string customer, string customerName, Country locale, Language language)
        {
            if (string.IsNullOrWhiteSpace(orderNumber) || orderNumber.Trim().Length < 4)
                throw new ArgumentException("Order number is mandatory");

            if (date == DateTime.MinValue || date == DateTime.MaxValue)
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

            if (items.Any(x => x.DueAmount == null || x.DueAmount.Value < 0)) // An item could costs 0 (a gift for example)
                throw new ArgumentException("Invalid item(s) detected");

            if (items.GroupBy(x => x.DueAmount.Currency).Select(x => x.Key).Distinct().Count() > 1)
                throw new ArgumentException("Multiply currencies have been detected in order items");

            _items = items;

            Currency itemsCurrency = items.GroupBy(x => x.DueAmount.Currency).Select(x => x.Key).Distinct().First();

            decimal maxRoundError = _locale == Country.India ? 10m : 1m;

            if (_total != null && (Math.Abs(items.Sum(x => x.TotalAmount.Value) - _total.Value) >= maxRoundError || _total.Currency != itemsCurrency))
                throw new ArgumentException("Order amount is not equals to order items summ or order currency different from order items");

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="total">Order due</param>
        /// <param name="paid">Money income</param>
        /// <param name="change">Change to be returned to the customer</param>
        /// <param name="points"></param>
        /// <returns></returns>
        public OrderBuilder WithTotalValues(Money total, Money paid, Money change, Money changeGiven, decimal points = 0)
        {
            if (total.Value < 0)
                throw new ArgumentException("Order amount must be positive");

            _total = total;
            _paid = paid ?? _total;
            _change = change;
            _changeGiven = changeGiven;
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

        public OrderBuilder WithInstallmentPayments(uint? installments)
        {
            _installmentPayments = installments;

            return this;
        }

        public Order Build()
        {
            if (_items == null || _items.Count() == 0)
                throw new ArgumentException("Items must be specified");

            if (_total.Value < 0)
                throw new ArgumentException("Order amount must be positive");

            if (string.IsNullOrWhiteSpace(_orderNumber) || _orderNumber.Trim().Length < 4)
                throw new ArgumentException("Order number is mandatory");

            if (string.IsNullOrWhiteSpace(_customer) || _customer.Trim().Length < 4)
                throw new ArgumentException("Customer is mandatory");

            return new Order {
                Items = _items,
                UncollectedItems = _uncollectedItems,
                Total = _total,
                Paid = _paid,
                Change = _change,
                ChangeGiven = _changeGiven,
                Customer = _customer,
                CustomerName = _customerName,
                Points = _points,
                Location = _locale,
                Language = _language,
                Number = _orderNumber,
                Date = _orderDate,
                Obtaining = _obtainingMethod,
                PaymentMethod = _paymentMethod,
                ExtraData = _extraData,
                Installments = _installmentPayments
            };
        }
    }
}
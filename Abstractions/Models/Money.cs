using Filuet.Infrastructure.Abstractions.Converters;
using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business
{
    public class Money : IEquatable<Money>
    {
        [JsonIgnore]
        public decimal Abs => Math.Abs(Value);

        public decimal Value { get; set; }

        [JsonConverter(typeof(CurrencyJsonConverter))]
        public Currency Currency { get; set; }

        public Money() { }

        public static Money Create(decimal value, Currency currency) => new Money { Value = value, Currency = currency };

        public static Money From(Money money) => Create(money.Value, money.Currency);

        public override string ToString()
        {
            if (Currency == 0)
                return string.Empty;

            return $"{Value:#,##0.00} {Currency.GetCode()}";
        }

        public string ToString(bool useCurrencySymbol, string format = "#,##0.00")
            => useCurrencySymbol && Currency != 0 ? $"{Currency.GetDescription()} {Value.ToString(format)}" : ToString();

        public static bool operator ==(Money obj1, Money obj2)
        {
            if (ReferenceEquals(obj1, obj2))
                return true;

            if (ReferenceEquals(obj1, null))
                return false;

            if (ReferenceEquals(obj2, null))
                return false;

            return obj1.Equals(obj2);
        }

        public static Money operator +(Money obj1, Money obj2)
        {
            if (ReferenceEquals(obj1, obj2))
                return Money.Create(obj1.Value * 2, obj1.Currency);

            if (ReferenceEquals(obj1, null))
                return obj2;

            if (ReferenceEquals(obj2, null))
                return obj1;

            if (obj1.Currency != obj2.Currency)
                throw new ArgumentException("Terms must be the same currency");

            return Money.Create(obj1.Value + obj2.Value, obj1.Currency);
        }

        public static Money operator -(Money obj1, Money obj2)
        {
            if (ReferenceEquals(obj1, obj2))
                return Money.Create(0m, obj1.Currency);

            if (ReferenceEquals(obj1, null))
                return obj2;

            if (ReferenceEquals(obj2, null))
                return obj1;

            if (obj1.Currency != obj2.Currency)
                throw new ArgumentException("Terms must be the same currency");

            return Money.Create(obj1.Value - obj2.Value, obj1.Currency);
        }

        public static Money operator +(Money obj1, decimal obj2)
        {
            if (ReferenceEquals(obj1, null))
                throw new ArgumentException("Invalid term");

            return Money.Create(obj1.Value + obj2, obj1.Currency);
        }

        public static Money operator -(Money obj1, decimal obj2)
        {
            if (ReferenceEquals(obj1, null))
                throw new ArgumentException("Invalid term");

            return Money.Create(obj1.Value - obj2, obj1.Currency);
        }

        public static bool operator >(Money obj1, Money obj2)
        {
            if (ReferenceEquals(obj1, obj2))
                return true;

            if (ReferenceEquals(obj1, null))
                return false;

            if (ReferenceEquals(obj2, null))
                return false;

            if (obj1.Currency != obj2.Currency)
                return false;

            return obj1.Value > obj2.Value;
        }

        public static bool operator <(Money obj1, Money obj2)
        {
            if (ReferenceEquals(obj1, obj2))
                return true;

            if (ReferenceEquals(obj1, null))
                return false;

            if (ReferenceEquals(obj2, null))
                return false;

            if (obj1.Currency != obj2.Currency)
                return false;

            return obj1.Value < obj2.Value;
        }

        public static bool operator >=(Money obj1, Money obj2)
        {
            if (ReferenceEquals(obj1, obj2))
                return true;
            
            if (ReferenceEquals(obj1, null))
                return false;
            
            if (ReferenceEquals(obj2, null))
                return false;
            
            if (obj1.Currency != obj2.Currency)
                return false;

            return obj1.Value >= obj2.Value;
        }

        public static bool operator <=(Money obj1, Money obj2)
        {
            if (ReferenceEquals(obj1, obj2))
                return true;

            if (ReferenceEquals(obj1, null))
                return false;

            if (ReferenceEquals(obj2, null))
                return false;

            if (obj1.Currency != obj2.Currency)
                return false;

            return obj1.Value <= obj2.Value;
        }

        public static bool operator !=(Money obj1, Money obj2) => !(obj1 == obj2);

        public static bool operator ==(Money obj1, decimal value)
        {
            if (obj1 == null)
                return false;

            return obj1.Value == value;
        }

        public static bool operator !=(Money obj1, decimal value) => !(obj1 == value);

        public static bool operator >(Money obj1, decimal value)
        {
            if (obj1 == null)
                return false;

            return obj1.Value > value;
        }

        public static bool operator <(Money obj1, decimal value)
        {
            if (obj1 == null)
                return false;

            return obj1.Value < value;
        }

        public static bool operator >=(Money obj1, decimal value)
        {
            if (obj1 == null)
                return false;

            return obj1.Value >= value;
        }

        public static bool operator <=(Money obj1, decimal value)
        {
            if (obj1 == null)
                return false;

            return obj1.Value <= value;
        }

        public bool Equals(Money other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Currency == other.Currency && Value.Equals(other.Value);
        }

        public override bool Equals(object obj) => Equals(obj as Money);

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Value.GetHashCode();
                hashCode = ((int)Currency) * 10000000 + hashCode;
                return hashCode;
            }
        }
    }
}
using Filuet.Infrastructure.Abstractions.Business;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Filuet.Infrastructure.Abstractions.Helpers
{
    public static class MoneyHelpers
    {
        public static Money Sum(this IEnumerable<Money> source)
            => source.Aggregate((x, y) => x + y);

        public static Money Sum<T>(this IEnumerable<T> source, Func<T, Money> selector)
            => source.Select(selector).Aggregate((x, y) => x + y);
    }
}
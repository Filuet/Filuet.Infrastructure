using System.Collections.Generic;

namespace Filuet.Infrastructure.Abstractions.Helpers
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
            => collection == null || collection.Count == 0;
    }
}
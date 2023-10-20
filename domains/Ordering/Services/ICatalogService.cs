using Filuet.Infrastructure.Abstractions.Enums;
using System.Collections.Generic;

namespace Filuet.Infrastructure.Ordering.Services
{
    public interface ICatalogService
    {
        /// <summary>
        /// Get product names by unique identifiers
        /// </summary>
        /// <param name="uids">sku</param>
        /// <param name="language"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        Dictionary<string, string> GetNames(IEnumerable<string> uids, Language language, Country country);

        /// <summary>
        /// Get products weight
        /// </summary>
        /// <param name="uids">sku</param>
        /// <param name="country">country</param>
        /// <returns></returns>
        Dictionary<string, int> GetWeights(IEnumerable<string> uids, Country country);
    }
}
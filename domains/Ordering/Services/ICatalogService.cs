using Filuet.Infrastructure.Abstractions.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<Dictionary<string, string>> GetNamesAsync(IEnumerable<string> uids, Language language, Country country);
        /// <summary>
        /// Get products weight
        /// </summary>
        /// <param name="uids">sku</param>
        /// <param name="country">country</param>
        /// <returns></returns>
        Task<Dictionary<string, int>> GetWeightsAsync(IEnumerable<string> uids, Country country);
    }
}
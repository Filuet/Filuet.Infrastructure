using Filuet.Infrastructure.Abstractions.Enums;

namespace Filuet.Infrastructure.Ordering.Services
{
    public interface ICatalogService
    {
        /// <summary>
        /// Get product name by it's unique identifier
        /// </summary>
        /// <param name="uid">sku</param>
        /// <param name="language"></param>
        /// <returns></returns>
        string GetName(string uid, Language language);
    }
}

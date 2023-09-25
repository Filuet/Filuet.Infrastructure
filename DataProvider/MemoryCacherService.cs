using Filuet.Infrastructure.DataProvider.Interfaces;
using System.Collections.Generic;

namespace Filuet.Infrastructure.DataProvider
{
    public class MemoryCachingService : IMemoryCachingService
    {
        public MemoryCacher Get(string cacherName, uint sizeMb = 1)
        {
            cacherName = cacherName.Trim();

            if (!string.IsNullOrWhiteSpace(cacherName) && !_cachers.ContainsKey(cacherName))
                _cachers.Add(cacherName, new MemoryCacher(sizeMb));

            return _cachers[cacherName];
        }

        private Dictionary<string, MemoryCacher> _cachers = new Dictionary<string, MemoryCacher>();
    }
}